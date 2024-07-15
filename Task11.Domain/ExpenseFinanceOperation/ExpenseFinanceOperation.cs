﻿using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperation.ValueObjects;

namespace Task11.Domain.ExpenseFinanceOperation
{
    public sealed class ExpenseFinanceOperation(ExpenseFinanceOperationId id,
                                                DateTime date,
                                                ExpenseTypeId expenseTypeId,
                                                Amount amount,
                                                string name) : AggregateRoot<ExpenseFinanceOperationId>(id)
    {
        public ExpenseTypeId ExpenseTypeId { get; private set; } = expenseTypeId;

        public DateTime Date { get; private set; } = date;

        public Amount Amount { get; private set; } = amount;

        public string Name { get; private set; } = name;
    }
}
