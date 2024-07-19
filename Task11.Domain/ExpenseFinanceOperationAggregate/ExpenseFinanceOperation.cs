﻿using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Domain.ExpenseFinanceOperationAggregate
{
    public sealed class ExpenseFinanceOperation(ExpenseFinanceOperationId id,
                                                DateOnly date,
                                                ExpenseTypeId expenseTypeId,
                                                Amount amount,
                                                string name) : AggregateRoot<ExpenseFinanceOperationId>(id)
    {
        public ExpenseTypeId ExpenseTypeId { get; private set; } = expenseTypeId;

        public DateOnly Date { get; private set; } = date;

        public Amount Amount { get; private set; } = amount;

        public string Name { get; private set; } = name;

        public void Update(DateOnly date, ExpenseTypeId expenseTypeId, Amount amount, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

            Date = date;
            ExpenseTypeId = expenseTypeId;
            Amount = amount;
            Name = name;
        }
    }
}
