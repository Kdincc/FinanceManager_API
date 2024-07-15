using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task11.Domain.Common.Сonstants;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Infrastructure.Persistence.Configurations
{
    internal class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.ToTable("ExpenseTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => ExpenseTypeId.Create(value));

            builder.Property(x => x.Name)
                .HasMaxLength(ValidationConstantst.OperationType.MaxNameLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(ValidationConstantst.OperationType.MaxDescriptionLength);
        }
    }
}
