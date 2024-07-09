﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Domain.FinanceOperationAggregate.ValueObjects
{
    public sealed class FinanceOperationId : ValueObject
    {
        private FinanceOperationId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public static FinanceOperationId CreateUniq() => new(Guid.NewGuid());

        public static FinanceOperationId Create(Guid value) => new(value);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
