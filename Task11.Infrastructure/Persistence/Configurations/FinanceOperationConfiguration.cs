using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.FinanceOperationAggregate;
using Task11.Domain.FinanceOperationAggregate.ValueObjects;

namespace Task11.Infrastructure.Persistence.Configurations
{
    public sealed class FinanceOperationConfiguration : IEntityTypeConfiguration<FinanceOperation>
    {
        public void Configure(EntityTypeBuilder<FinanceOperation> builder)
        {
            ConfigureFinanceOperationsTable(builder);
        }

        private void ConfigureFinanceOperationsTable(EntityTypeBuilder<FinanceOperation> builder)
        {
            builder.ToTable("FinanceOperations");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => FinanceOperationId.Create(value));
        }
    }
}
