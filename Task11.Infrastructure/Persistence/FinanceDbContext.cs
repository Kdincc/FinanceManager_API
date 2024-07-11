using Microsoft.EntityFrameworkCore;
using Task11.Domain.ExpenseFinanceOperation;
using Task11.Domain.ExpenseType;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeType;
using Task11.Infrastructure.Persistence.Configurations;

namespace Task11.Infrastructure.Persistence
{
    public sealed class FinanceDbContext(DbContextOptions<FinanceDbContext> options) : DbContext(options)
    {
        public DbSet<IncomeFinanceOperation> IncomeFinanceOperations { get; set; }

        public DbSet<ExpenseFinanceOperation> ExpenseFinanceOperations { get; set; }

        public DbSet<IncomeType> IncomeTypes { get; set; }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
