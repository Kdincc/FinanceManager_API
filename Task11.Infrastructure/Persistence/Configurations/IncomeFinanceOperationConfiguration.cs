using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Infrastructure.Persistence.Configurations
{
    internal class IncomeFinanceOperationConfiguration : IEntityTypeConfiguration<IncomeFinanceOperation>
    {
        public void Configure(EntityTypeBuilder<IncomeFinanceOperation> builder)
        {
            builder.ToTable("IncomeFinanceOperations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => IncomeFinanceOperationId.Create(value));

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Amount)
                .HasConversion(amount => amount.Value, value => Amount.Create(value));

            builder.Property(x => x.IncomeTypeId)
                .HasConversion(id => id.Value, value => IncomeTypeId.Create(value));

            builder.HasOne<IncomeType>()
                .WithMany()
                .HasForeignKey(x => x.IncomeTypeId);
        }
    }
}
