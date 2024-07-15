using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class ExpenseType
        {
            public static Error ExpenseTypeNotFound => Error.NotFound(
                code: "ExpenseType.NotFound",
                description: "Expense type with that id not found!");

            public static Error DuplicateExpenseType => Error.Conflict(
                code: "Expense.DuplicateIncomeType",
                description: "Expense type with same name and description already exists!");
        }
            
    }
}
