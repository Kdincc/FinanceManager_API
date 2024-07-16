using Mapster;
using Task11.Application.ExpenseTypes.Commands.Delete;
using Task11.Application.ExpenseTypes.Commands.Update;
using Task11.Contracts.ExpenseType;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenseTypesMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<DeleteExpenseTypeRequest, DeleteExpenseTypeCommand>()
                .Map(dest => dest.ExpenseTypeId, src => ExpenseTypeId.Create(Guid.Parse(src.Id)))
                .ConstructUsing(src => new DeleteExpenseTypeCommand(ExpenseTypeId.Create(Guid.Parse(src.Id))));

            config.ForType<UpdateExpenseTypeRequest, UpdateExpenseTypeCommand>()
                .Map(dest => dest.ExpenseTypeId, src => ExpenseTypeId.Create(Guid.Parse(src.Id)))
                .ConstructUsing(src => new UpdateExpenseTypeCommand(ExpenseTypeId.Create(Guid.Parse(src.Id)), src.Name, src.Description));
        }
    }
}
