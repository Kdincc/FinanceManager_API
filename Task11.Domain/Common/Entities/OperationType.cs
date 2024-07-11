using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Domain.Common.Entities
{
    public abstract class OperationType<TId>(TId id, string name, string description) : AggregateRoot<TId>(id) where TId : ValueObject
    {
        public string Name { get; private set; } = name;

        public string Description { get; private set; } = description;

        public string ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentNullException(nameof(newName));
            }

            if ()

            Name = newName;
        }
    }
}
