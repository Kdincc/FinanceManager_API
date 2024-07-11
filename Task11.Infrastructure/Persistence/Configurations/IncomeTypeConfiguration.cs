using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.IncomeType;

namespace Task11.Infrastructure.Persistence.Configurations
{
    internal class IncomeTypeConfiguration : IEntityTypeConfiguration<IncomeType>
    {
        public void Configure(EntityTypeBuilder<IncomeType> builder)
        {
            builder.ToTable("IncomeTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => IncomeTypeId.Create(value));

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(255);
        }
    }
}
