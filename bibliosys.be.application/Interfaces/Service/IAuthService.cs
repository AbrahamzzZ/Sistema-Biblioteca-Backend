using System.Threading.Tasks;

namespace bibliosys.be.application.Interfaces.Service
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string email, string password);
    }
}

