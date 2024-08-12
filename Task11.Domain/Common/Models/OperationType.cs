using Task11.Domain.Common.Сonstants;

namespace Task11.Domain.Common.Models
{
    public abstract class OperationType<TId>(TId id, string name, string description) : AggregateRoot<TId>(id) where TId : ValueObject
    {
        public string Name { get; private set; } = name;

        public string Description { get; private set; } = description;

        public bool HasSameNameAndDescription(OperationType<TId> operationType)
        {
            return Name == operationType.Name && Description == operationType.Description;
        }

        public void Update(string name, string description)
        {
            ChangeName(name);
            ChangeDescription(description);
        }

        private void ChangeName(string newName)
        {
            ArgumentException.ThrowIfNullOrEmpty(newName, nameof(newName));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(newName.Length, ValidationConstants.OperationType.MaxNameLength);

            Name = newName;
        }

        private void ChangeDescription(string newDescription)
        {
            ArgumentException.ThrowIfNullOrEmpty(newDescription, nameof(newDescription));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(newDescription.Length, ValidationConstants.OperationType.MaxDescriptionLength);

            Description = newDescription;
        }
    }
}
