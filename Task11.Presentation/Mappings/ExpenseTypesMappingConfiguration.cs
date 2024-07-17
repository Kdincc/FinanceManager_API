using Mapster;
using Task11.Application.ExpenseTypes.Commands.Delete;
using Task11.Application.ExpenseTypes.Commands.Update;
using Task11.Contracts.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenseTypesMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<DeleteExpenseTypeRequest, DeleteExpenseTypeCommand>()
                .ConstructUsing(src => new DeleteExpenseTypeCommand(ExpenseTypeId.Create(Guid.Parse(src.Id))));

            config.ForType<UpdateExpenseTypeRequest, UpdateExpenseTypeCommand>()
                .ConstructUsing(src => new UpdateExpenseTypeCommand(ExpenseTypeId.Create(Guid.Parse(src.Id)), src.Name, src.Description));
        }
    }
}
