using System.Collections.Generic;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;

namespace bibliosys.be.domain.Repositories
{
    public interface ILibroRepository
    {
        Task<Libro?> GetByIdAsync(int id);
        Task<IEnumerable<Libro>> GetAllActivosAsync();
        Task<IEnumerable<Libro>> GetAllAsync();
        Task AddAsync(Libro libro);
        Task UpdateAsync(Libro libro);
    }
}

