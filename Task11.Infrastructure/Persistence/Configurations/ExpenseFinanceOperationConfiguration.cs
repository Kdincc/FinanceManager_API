using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Infrastructure.Persistence.Configurations
{
    internal class ExpenseFinanceOperationConfiguration : IEntityTypeConfiguration<ExpenseFinanceOperation>
    {
        public void Configure(EntityTypeBuilder<ExpenseFinanceOperation> builder)
        {
            builder.ToTable("ExpenseFinanceOperations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => ExpenseFinanceOperationId.Create(value.ToString()));

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Amount)
                .HasConversion(amount => amount.Value, value => Amount.Create(value));

            builder.Property(x => x.ExpenseTypeId)
                .HasConversion(id => id.Value, value => ExpenseTypeId.Create(value.ToString()));

            builder.HasOne<ExpenseType>()
                .WithMany()
                .HasForeignKey(x => x.ExpenseTypeId);
        }
    }
}
