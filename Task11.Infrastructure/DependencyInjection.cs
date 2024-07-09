﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.Entities;
using Task11.Infrastructure.Persistence;
using Task11.Infrastructure.Persistence.Repos;

namespace Task11.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfarstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbString"), b => b.MigrationsAssembly("Task11.Presentation")));
            
            services.AddScoped<IRepository<IncomeType>, IncomeTypeRepository>();

            return services;
        }
    }
}
