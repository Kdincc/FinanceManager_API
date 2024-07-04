using Task11.Domain.Common.Models;
using Task11.Domain.FinanceOperationAggregate.Entities;
using Task11.Domain.FinanceOperationAggregate.ValueObjects;

namespace Task11.Domain.FinanceOperationAggregate
{
    public sealed class FinanceOperation : AggregateRoot<FinanceOperationId>
    {
        public FinanceOperation(FinanceOperationId id, DateTime date, OperationType<ValueObject> operationType, Amount amount) : base(id)
        {
            Id = id;
            Date = date;
            OperationType = operationType;
            Amount = amount;
        }

        public DateTime Date { get; }
        public OperationType<ValueObject> OperationType { get; }
        public Amount Amount { get; }
    }
}