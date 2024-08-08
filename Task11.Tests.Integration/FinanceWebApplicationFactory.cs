using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Infrastructure.Persistence;
using Task11.Presentation;

namespace Task11.Tests.Integration
{
    public sealed class FinanceWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FinanceDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<FinanceDbContext>(options =>
                {
                    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TEST;Integrated Security=True;Trust Server Certificate=True");
                });

                var context = CreateDbContext(services);

                context.Database.EnsureCreated();
            });

        }

        public void ResetDatabase()
        {
            using var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<FinanceDbContext>();

            db.IncomeTypes.RemoveRange(db.IncomeTypes);
            db.ExpenseTypes.RemoveRange(db.ExpenseTypes);
            db.IncomeFinanceOperations.RemoveRange(db.IncomeFinanceOperations);
            db.ExpenseFinanceOperations.RemoveRange(db.ExpenseFinanceOperations);

            db.SaveChanges();
        }

        private FinanceDbContext CreateDbContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();

            return scope.ServiceProvider.GetRequiredService<FinanceDbContext>();
        }
    }
}
