using Task11.Domain.Common.Models;
using Task11.Domain.FinanceOperationAggregate.ValueObjects;

namespace Task11.Domain.FinanceOperationAggregate
{
    public sealed class FinanceOperation : AggregateRoot<FinanceOperationId>
    {
        private FinanceOperation(FinanceOperationId id, DateTime date) : base(id)
        {
        }
    }
}