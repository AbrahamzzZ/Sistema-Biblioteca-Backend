using System.Collections.Generic;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;

namespace bibliosys.be.domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllActivosAsync();
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
    }
}

