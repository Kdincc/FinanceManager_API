using Mapster;
using Task11.Application.IncomeTypes;
using Task11.Application.IncomeTypes.Commands.Create;
using Task11.Application.IncomeTypes.Commands.Delete;
using Task11.Application.IncomeTypes.Commands.Update;
using Task11.Contracts.IncomeType;

namespace Task11.Presentation.Mappings
{
    public sealed class IncomeTypesMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateIncomeTypeCommand, CreateIncomeTypeRequest>();

            config.NewConfig<UpdateIncomeTypeCommand, UpdateIncomeTypeRequest>();

            config.NewConfig<DeleteIncomeTypeCommand, DeleteIncomeTypeRequest>();
        }
    }
}
