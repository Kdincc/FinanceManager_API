using Mapster;
using Task11.Application.IncomeTypes.Commands.Create;
using Task11.Application.IncomeTypes.Commands.Delete;
using Task11.Application.IncomeTypes.Commands.Update;
using Task11.Contracts.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Presentation.Mappings
{
    public sealed class IncomeTypesMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateIncomeTypeRequest, CreateIncomeTypeCommand>();

            config.NewConfig<DeleteIncomeTypeRequest, DeleteIncomeTypeCommand>();

            config.NewConfig<UpdateIncomeTypeRequest, UpdateIncomeTypeCommand>();
        }
    }
}
