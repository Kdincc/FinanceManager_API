using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Application.Common.Persistance
{
    public interface IRepository<T> where T : notnull
    {
        public IReadOnlyCollection<T> GetAll();

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);
    }
}
