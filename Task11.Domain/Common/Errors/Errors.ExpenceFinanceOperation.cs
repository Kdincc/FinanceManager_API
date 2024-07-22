using ErrorOr;

namespace Task11.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class ExpenceFinanceOperation
        {
            public static Error ExpenceFinanceOperationNotFound => Error.NotFound(
                code: "ExpenceFinanceOperation.NotFound",
                description: "Expence finance operation with that id not found!");

            public static Error ExpenseFinanceOperationDateNotFound => Error.NotFound(
                code: "ExpenseFinanceOperation.DateNotFound",
                description: "Expense finance operation with that date not found!"); 
        }
    }
}
