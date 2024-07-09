using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task11.Infrastructure.Persistence;

namespace Task11.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfarstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DbString"), b => b.MigrationsAssembly("Task11.Presentation")));

            return services;
        }
    }
}
