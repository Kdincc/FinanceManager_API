using Task11.Domain.Common.Models;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.Common.Сonstants;

namespace Task11.Domain.Common.Entities
{
    public abstract class OperationType<TId>(TId id, string name, string description, Amount amount) : AggregateRoot<TId>(id) where TId : ValueObject
    {
        public string Name { get; private set; } = name;

        public string Description { get; private set; } = description;

        public Amount Amount { get; private set; } = amount;


        public bool HasSameNameAndDescription(OperationType<TId> operationType)
        {
            return Name == operationType.Name && Description == operationType.Description;
        }

        public void Update(string name, string description, Amount amount)
        {
            ChangeName(name);
            ChangeDescription(description);
            ChangeAmount(amount);
        }

        private void ChangeName(string newName)
        {
            ArgumentException.ThrowIfNullOrEmpty(newName, nameof(newName));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(newName.Length, ValidationConstantst.OperationType.MaxNameLength);

            Name = newName;
        }

        private void ChangeDescription(string newDescription)
        {
            ArgumentException.ThrowIfNullOrEmpty(newDescription, nameof(newDescription));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(newDescription.Length, ValidationConstantst.OperationType.MaxDescriptionLength);

            Description = newDescription;
        }

        private void ChangeAmount(Amount newAmount)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(newAmount.Value, 0);

            Amount = newAmount;
        }
    }
}
