using Mapster;
using Task11.Application.ExpenseTypes.Commands.Create;
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
            config.NewConfig<CreateExpenseTypeRequest, CreateExpenseTypeCommand>();

            config.NewConfig<DeleteExpenseTypeRequest, DeleteExpenseTypeCommand>();

            config.NewConfig<UpdateExpenseTypeRequest, UpdateExpenseTypeCommand>();
        }
    }
}
