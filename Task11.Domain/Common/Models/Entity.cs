using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Domain.Common.Models
{
    public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>> where TId : notnull, ValueObject
    {
        public TId Id { get; protected set; }

        public bool Equals(Entity<TId> other)
        {
            return Equals(other as object);
        }

        public override bool Equals(object obj)
        {
            if (obj is null or not Entity<TId>)
            {
                return false;
            }

            var entity = obj as Entity<TId>;

            return Id.Equals(entity.Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id.GetHashCode());
        }
    }
}
