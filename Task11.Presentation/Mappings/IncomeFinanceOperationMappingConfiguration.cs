using Mapster;
using Task11.Application.IncomeFinanceOperations.Commands.Create;
using Task11.Application.IncomeFinanceOperations.Commands.Delete;
using Task11.Application.IncomeFinanceOperations.Commands.Update;
using Task11.Contracts.IncomeFinanceOperation;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

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
