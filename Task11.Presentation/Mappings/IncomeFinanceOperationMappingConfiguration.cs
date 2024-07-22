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
            config.ForType<CreateIncomeFinanceOperationRequest, CreateIncomeFinanceOperationCommand>()
                .ConstructUsing(src => new CreateIncomeFinanceOperationCommand(
                    DateOnly.Parse(src.Date),
                    IncomeTypeId.Create(Guid.Parse(src.IncomeTypeId)),
                    Amount.Create(src.Amount),
                    src.Name));

            config.ForType<UpdateIncomeFinanceOperationRequest, UpdateIncomeFinanceOperationCommand>()
                .ConstructUsing(src => new UpdateIncomeFinanceOperationCommand(
                    IncomeFinanceOperationId.Create(Guid.Parse(src.IncomeFinanceOperationId)),
                    IncomeTypeId.Create(Guid.Parse(src.IncomeTypeId)),
                    DateOnly.Parse(src.Date),
                    Amount.Create(src.Amount),
                    src.Name));

            config.ForType<DeleteIncomeFinanceOperationRequest, DeleteIncomeFinanceOperationCommand>()
                .ConstructUsing(src => new DeleteIncomeFinanceOperationCommand(IncomeFinanceOperationId.Create(Guid.Parse(src.IncomeFinanceOperationId))));
        }
    }
}
