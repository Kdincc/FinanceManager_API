using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.Common.Models;

namespace Task11.Domain.Reports
{
    internal class PeriodReport : ValueObject
    {
        public override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
