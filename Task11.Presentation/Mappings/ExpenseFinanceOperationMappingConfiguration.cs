using Mapster;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Application.ExpenseFinanceOperations.Commands.Delete;
using Task11.Application.ExpenseFinanceOperations.Commands.Update;
using Task11.Contracts.ExpenseFinanceOperation;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenseFinanceOperationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateExpenseFinanceOperationRequest, CreateExpenseFinanaceOperationCommand>();

            config.NewConfig<UpdateExpenseFinanceOperationRequest, UpdateExpenceFinanceOperationCommand>();

            config.NewConfig<DeleteExpenseFinanceOperationRequest, DeleteExpenceFinanceOperationCommand>();
        }
    }
}
