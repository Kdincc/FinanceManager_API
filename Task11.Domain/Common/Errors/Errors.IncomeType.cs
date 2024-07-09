using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeFinanceOperation.Entities;

namespace Task11.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class IncomeType
        {
            public static Error IncomeTypeNotFound => Error.NotFound(
                code: "IncomeType.NotFound",
                description: "Income type with that id not found!");

            public static Error DuplicateIncomeType => Error.Conflict(
                code: "IncomeType.DuplicateIncomeType", 
                description: "Income type with same name and description already exists!")
        }
            
    }
}
