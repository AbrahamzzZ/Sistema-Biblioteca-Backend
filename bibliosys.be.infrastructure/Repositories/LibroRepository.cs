using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;
using bibliosys.be.infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace bibliosys.be.infrastructure.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly DBContext _context;

        public LibroRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Libro?> GetByIdAsync(int id)
        {
            return _context.Libros.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Libro>> GetAllActivosAsync()
        {
            return await _context.Libros
                .Where(l => l.Estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Libro>> GetAllAsync()
        {
            return await _context.Libros.ToListAsync();
        }

        public async Task AddAsync(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Libro libro)
        {
            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();
        }
    }
}

