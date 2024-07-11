using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;
using Task11.Domain.Common.Сonstants;

namespace Task11.Domain.Common.Entities
{
    public abstract class OperationType<TId>(TId id, string name, string description) : AggregateRoot<TId>(id) where TId : ValueObject
    {
        public string Name { get; private set; } = name;

        public string Description { get; private set; } = description;

        public void ChangeName(string newName)
        {
            ArgumentException.ThrowIfNullOrEmpty(newName, nameof(newName));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(newName.Length, ValidationConstantst.OperationType.MaxNameLength);

            Name = newName;
        }

        public void ChangeDescription(string newDescription)
        {
            ArgumentException.ThrowIfNullOrEmpty(newDescription, nameof(newDescription));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(newDescription.Length, ValidationConstantst.OperationType.MaxDescriptionLength);

            Description = newDescription;
        }
    }
}
