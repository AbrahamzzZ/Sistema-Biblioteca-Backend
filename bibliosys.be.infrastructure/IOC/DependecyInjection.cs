using bibliosys.be.application.Interfaces.Service;
using bibliosys.be.application.Models;
using bibliosys.be.application.Services;
using bibliosys.be.domain.Repositories;
using bibliosys.be.infrastructure.Database;
using bibliosys.be.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bibliosys.be.infrastructure.IOC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IBibliotecarioRepository, BibliotecarioRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPrestamoService, PrestamoService>();

            return services;
        }
    }
}
