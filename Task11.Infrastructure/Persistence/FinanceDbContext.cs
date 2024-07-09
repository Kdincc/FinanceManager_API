using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.FinanceOperationAggregate;

namespace Task11.Infrastructure.Persistence
{
    public sealed class FinanceDbContext(DbContextOptions<FinanceDbContext> options) : DbContext(options)
    {
        public DbSet<FinanceOperation> FinanceOperations { get; set; }
    }
}
