﻿using Task11.Domain.Common.Models;

namespace Task11.Domain.IncomeFinanceOperation.ValueObjects
{
    public sealed class IncomeFinanceOperationId : ValueObject
    {
        private IncomeFinanceOperationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static IncomeFinanceOperationId CreateUniq() => new(Guid.NewGuid());

        public static IncomeFinanceOperationId Create(Guid value) => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
