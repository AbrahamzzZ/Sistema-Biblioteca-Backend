using System.Threading.Tasks;
using bibliosys.be.domain.Entity;

namespace bibliosys.be.domain.Repositories
{
    public interface IBibliotecarioRepository
    {
        Task<Bibliotecario?> GetByEmailAsync(string email);
    }
}

