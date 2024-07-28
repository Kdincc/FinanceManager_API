using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.Common.Persistance
{
    public interface IExpenseTypeRepository : IRepository<ExpenseType, ExpenseTypeId>
    {
    }
}
