using System.Threading.Tasks;
using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;
using bibliosys.be.infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace bibliosys.be.infrastructure.Repositories
{
    public class BibliotecarioRepository : IBibliotecarioRepository
    {
        private readonly DBContext _context;

        public BibliotecarioRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Bibliotecario?> GetByEmailAsync(string email)
        {
            return _context.Bibliotecarios.FirstOrDefaultAsync(b => b.Email == email);
        }
    }
}

