using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Domain.Common.Models
{
    public abstract class AggregateRoot<TId>(TId id) : Entity<TId>(id) where TId : notnull, ValueObject
    {
    }
}
