using System.Collections.Generic;
using System.Threading.Tasks;
using bibliosys.be.domain.Entity;

namespace bibliosys.be.domain.Repositories
{
    public interface IPrestamoRepository
    {
        Task<Prestamo?> GetByIdAsync(int id);
        Task<IEnumerable<Prestamo>> GetByUsuarioAsync(int usuarioId);
        Task AddAsync(Prestamo prestamo);
        Task UpdateAsync(Prestamo prestamo);
    }
}

