using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Task11.Application.Common.Persistance;
using Task11.Infrastructure.Persistence;
using Task11.Infrastructure.Persistence.Repos;

namespace Task11.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfarstructure(this IServiceCollection services, IConfiguration configuration)
        {
            var asseblyName = Assembly.GetAssembly(typeof(DependencyInjection)).GetName().Name;

            services.AddDbContext<FinanceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbString"),
            b => b.MigrationsAssembly(asseblyName)));

            services.AddScoped<IIncomeTypeRepository, IncomeTypeRepository>();
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddScoped<IExpenseFinanceOperationRepository, ExpenceFinanceOperationRepository>();
            services.AddScoped<IIncomeFinanceOperationRepository, IncomeFinanceOperationRepository>();

            return services;
        }
    }
}
