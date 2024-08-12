using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task11.Domain.Common.Сonstants;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

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
                .HasConversion(id => id.Value, value => IncomeTypeId.Create(value.ToString()));

            builder.Property(x => x.Name)
                .HasMaxLength(ValidationConstants.OperationType.MaxNameLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(ValidationConstants.OperationType.MaxDescriptionLength);
        }
    }
}
