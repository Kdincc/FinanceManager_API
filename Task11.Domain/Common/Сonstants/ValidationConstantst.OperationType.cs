using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Domain.Common.Сonstants
{
    public static partial class ValidationConstantst
    {
        public static class OperationType
        {
            public static int MaxNameLength => 100;

            public static int MaxDescriptionLength => 255;
        }
    }
}
