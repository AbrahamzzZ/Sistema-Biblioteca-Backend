using System;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;

namespace bibliosys.be.application.Interfaces.Service
{
    public interface IPrestamoService
    {
        Task<Prestamo> CrearPrestamoAsync(int usuarioId, int libroId, DateTime fechaPrestamo, DateTime fechaLimite);
        Task RegistrarDevolucionAsync(int prestamoId, DateTime fechaDevolucion);
        Task AnularPrestamoAsync(int prestamoId, string? observacion = null);
    }
}

