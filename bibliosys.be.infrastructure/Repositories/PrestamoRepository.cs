using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;
using bibliosys.be.infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace bibliosys.be.infrastructure.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly DBContext _context;

        public PrestamoRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Prestamo?> GetByIdAsync(int id)
        {
            return _context.Prestamos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prestamo>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.Prestamos
                .Where(p => p.Usuario_Id == usuarioId)
                .ToListAsync();
        }

        public async Task AddAsync(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Prestamo prestamo)
        {
            _context.Prestamos.Update(prestamo);
            await _context.SaveChangesAsync();
        }
    }
}

