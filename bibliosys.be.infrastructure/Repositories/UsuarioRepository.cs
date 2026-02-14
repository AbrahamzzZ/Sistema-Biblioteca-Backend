using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;
using bibliosys.be.infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace bibliosys.be.infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DBContext _context;

        public UsuarioRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Usuario?> GetByIdAsync(int id)
        {
            return _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> GetAllActivosAsync()
        {
            return await _context.Usuarios
                .Where(u => u.Estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}

