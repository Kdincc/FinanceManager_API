using Mapster;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Contracts.ExpenceFinanceOperation;

namespace Task11.Presentation.Mappings
{
    public sealed class ExpenceFinanceOperationMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateExpenceFinanceOperationRequest, CreateExpenseFinanaceOperationCommand>()
                .Map(dest => dest.Date, src => src.Date);
        }
    }
}
