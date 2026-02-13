using System;
using System.Threading.Tasks;
using bibliosys.be.application.Interfaces.Service;
using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;

namespace bibliosys.be.application.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILibroRepository _libroRepository;
        private readonly IPrestamoRepository _prestamoRepository;

        public PrestamoService(
            IUsuarioRepository usuarioRepository,
            ILibroRepository libroRepository,
            IPrestamoRepository prestamoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _libroRepository = libroRepository;
            _prestamoRepository = prestamoRepository;
        }

        public async Task<Prestamo> CrearPrestamoAsync(int usuarioId, int libroId, DateTime fechaPrestamo, DateTime fechaLimite)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null || !usuario.Estado)
            {
                throw new InvalidOperationException("Usuario no válido o inactivo.");
            }

            var libro = await _libroRepository.GetByIdAsync(libroId);
            if (libro == null || !libro.Estado)
            {
                throw new InvalidOperationException("Libro no válido o inactivo.");
            }

            if (libro.Stock <= 0)
            {
                throw new InvalidOperationException("No hay stock disponible.");
            }

            var prestamo = new Prestamo
            {
                Usuario_Id = usuarioId,
                Libro_Id = libroId,
                Fecha_Prestamo = fechaPrestamo,
                Fecha_Limite_Devolucion = fechaLimite,
                Estado = true
            };

            libro.Stock -= 1;

            await _prestamoRepository.AddAsync(prestamo);
            await _libroRepository.UpdateAsync(libro);

            return prestamo;
        }

        public async Task RegistrarDevolucionAsync(int prestamoId, DateTime fechaDevolucion)
        {
            var prestamo = await _prestamoRepository.GetByIdAsync(prestamoId);
            if (prestamo == null)
            {
                throw new InvalidOperationException("Préstamo no encontrado.");
            }

            if (!prestamo.Estado)
            {
                throw new InvalidOperationException("El préstamo ya está devuelto.");
            }

            var libro = await _libroRepository.GetByIdAsync(prestamo.Libro_Id);
            if (libro == null)
            {
                throw new InvalidOperationException("Libro no encontrado.");
            }

            prestamo.Fecha_Real_Devolucion = fechaDevolucion;
            prestamo.Estado = false;
            libro.Stock += 1;

            await _prestamoRepository.UpdateAsync(prestamo);
            await _libroRepository.UpdateAsync(libro);
        }

        public async Task AnularPrestamoAsync(int prestamoId, string? observacion = null)
        {
            var prestamo = await _prestamoRepository.GetByIdAsync(prestamoId);
            if (prestamo == null)
            {
                throw new InvalidOperationException("Préstamo no encontrado.");
            }

            if (!prestamo.Estado)
            {
                // Ya devuelto o anulado
                return;
            }

            var libro = await _libroRepository.GetByIdAsync(prestamo.Libro_Id);
            if (libro == null)
            {
                throw new InvalidOperationException("Libro no encontrado.");
            }

            prestamo.Estado = false;
            prestamo.Observacion = observacion ?? "Préstamo anulado.";
            libro.Stock += 1;

            await _prestamoRepository.UpdateAsync(prestamo);
            await _libroRepository.UpdateAsync(libro);
        }
    }
}

