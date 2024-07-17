using Mapster;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Application.ExpenseFinanceOperations.Commands.Delete;
using Task11.Application.ExpenseFinanceOperations.Commands.Update;
using Task11.Contracts.ExpenseFinanceOperation;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;
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

            config.ForType<UpdateExpenseFinanceOperationRequest, UpdateExpenceFinanceOperationCommand>()
                .ConstructUsing(src => new UpdateExpenceFinanceOperationCommand(
                    ExpenseFinanceOperationId.Create(Guid.Parse(src.ExpenseFinanceOperationId)),
                    src.Date,
                    ExpenseTypeId.Create(Guid.Parse(src.ExpenseTypeId)),
                    Amount.Create(src.Amount),
                    src.Name));

            config.ForType<DeleteExpenseFinanceOperationRequest, DeleteExpenceFinanseOperationCommand>()
                .ConstructUsing(src => new DeleteExpenceFinanseOperationCommand(ExpenseFinanceOperationId.Create(Guid.Parse(src.ExpenseFinanceOperationId))));
        }
    }
}
