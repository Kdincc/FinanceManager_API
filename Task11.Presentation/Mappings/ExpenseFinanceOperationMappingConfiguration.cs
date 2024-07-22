using Mapster;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Application.ExpenseFinanceOperations.Commands.Delete;
using Task11.Application.ExpenseFinanceOperations.Commands.Update;
using Task11.Contracts.ExpenseFinanceOperation;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenseFinanceOperationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateExpenseFinanceOperationRequest, CreateExpenseFinanaceOperationCommand>();

            config.NewConfig<UpdateExpenseFinanceOperationRequest, UpdateExpenceFinanceOperationCommand>();

            config.NewConfig<DeleteExpenseFinanceOperationRequest, DeleteExpenceFinanseOperationCommand>();
        }
    }
}
