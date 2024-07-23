using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Domain.Common.Models
{
    public abstract class FinanceOperation<TId>(TId id, string name, Amount amount, DateOnly date) : AggregateRoot<TId>(id) where TId : ValueObject
    {
        public string Name { get; protected set; } = name;

        public Amount Amount { get; protected set; } = amount;

        public DateOnly Date { get; protected set; } = date;
    }
}
