using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.ValueObjects;

namespace Task11.Domain.Common.Models
{
    public abstract class FinanceOperation<TId>(TId id, string name, Amount amount, DateOnly date) : AggregateRoot<TId>(Id) where TId : ValueObject
    {
        public TId Id { get; private set; } = id;

        public string Name { get; private set; } = name;

        public Amount Amount { get; private set; } = amount;

        public DateOnly Date { get; private set; } = date;
    }
}
