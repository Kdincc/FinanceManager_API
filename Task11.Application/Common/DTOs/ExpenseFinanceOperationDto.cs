﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.Common.DTOs
{
    public sealed class ExpenseFinanceOperationDto(ExpenseFinanceOperation expenseFinanceOperation)
    {
        public ExpenseFinanceOperationId Id => expenseFinanceOperation.Id;

        public DateTime Date => expenseFinanceOperation.Date;

        public Amount Amount => expenseFinanceOperation.Amount;

        public string Name => expenseFinanceOperation.Name;

        public ExpenseTypeId ExpenseTypeId => expenseFinanceOperation.ExpenseTypeId;
    }
}
