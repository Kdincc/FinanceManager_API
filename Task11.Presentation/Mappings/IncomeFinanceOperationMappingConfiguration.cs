using Mapster;
using Task11.Application.IncomeFinanceOperations.Commands.Create;
using Task11.Application.IncomeFinanceOperations.Commands.Delete;
using Task11.Application.IncomeFinanceOperations.Commands.Update;
using Task11.Contracts.IncomeFinanceOperation;

namespace Task11.Presentation.Mappings
{
    public sealed class IncomeFinanceOperationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateIncomeFinanceOperationRequest, CreateIncomeFinanceOperationCommand>();

            config.NewConfig<UpdateIncomeFinanceOperationRequest, UpdateIncomeFinanceOperationCommand>();

            config.NewConfig<DeleteIncomeFinanceOperationRequest, DeleteIncomeFinanceOperationCommand>();
        }
    }
}
