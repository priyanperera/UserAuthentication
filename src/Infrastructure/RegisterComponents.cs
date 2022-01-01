using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class RegisterComponents
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));

            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

            return services;
        }
    }
}
