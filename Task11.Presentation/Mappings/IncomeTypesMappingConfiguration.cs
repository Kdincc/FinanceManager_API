using Mapster;
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
            config.ForType<DeleteIncomeTypeRequest, DeleteIncomeTypeCommand>()
                .ConstructUsing(src => new DeleteIncomeTypeCommand(IncomeTypeId.Create(Guid.Parse(src.Id))));

            config.ForType<UpdateIncomeTypeRequest, UpdateIncomeTypeCommand>()
                .ConstructUsing(src => new UpdateIncomeTypeCommand(IncomeTypeId.Create(Guid.Parse(src.Id)), src.Name, src.Description));
        }
    }
}
