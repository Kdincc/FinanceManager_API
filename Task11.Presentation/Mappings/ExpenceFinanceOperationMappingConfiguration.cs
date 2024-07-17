using Mapster;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Contracts.ExpenceFinanceOperation;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenceFinanceOperationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateExpenceFinanceOperationRequest, CreateExpenseFinanaceOperationCommand>()
                .ConstructUsing(src => new CreateExpenseFinanaceOperationCommand(
                    src.Date,
                    ExpenseTypeId.Create(Guid.Parse(src.ExpenceTypeId)),
                    Amount.Create(src.Amount),
                    src.Name));
        }
    }
}
