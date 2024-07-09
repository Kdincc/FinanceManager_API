using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeFinanceOperation.Entities;
using Task11.Infrastructure.Persistence.Configurations;

namespace Task11.Infrastructure.Persistence
{
    public sealed class FinanceDbContext(DbContextOptions<FinanceDbContext> options) : DbContext(options)
    {
        public DbSet<IncomeFinanceOperation> IncomeFinanceOperations { get; set; }

        public DbSet<IncomeType> IncomeTypes { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IncomeFinanceOperationConfiguration());
            modelBuilder.ApplyConfiguration(new IncomeTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
