using Mapster;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Contracts.ExpenseFinanceOperation;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenseFinanceOperationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateExpenseFinanceOperationRequest, CreateExpenseFinanaceOperationCommand>()
                .ConstructUsing(src => new CreateExpenseFinanaceOperationCommand(
                    src.Date,
                    ExpenseTypeId.Create(Guid.Parse(src.ExpenseTypeId)),
                    Amount.Create(src.Amount),
                    src.Name));
        }
    }
}
