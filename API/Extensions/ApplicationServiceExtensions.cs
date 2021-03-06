using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("DbConnection")));

            services.AddScoped<ITokenService, TokenService>();
            
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        
            return services;
        }
    }
}