using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task11.Application.Common.Persistance;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Infrastructure.Persistence;
using Task11.Infrastructure.Persistence.Repos;

namespace Task11.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfarstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbString"), b => b.MigrationsAssembly("Task11.Presentation")));
            
            services.AddScoped<IRepository<IncomeType, IncomeTypeId>, IncomeTypeRepository>();
            services.AddScoped<IRepository<ExpenseType, ExpenseTypeId>, ExpenseTypeRepository>();

            return services;
        }
    }
}
