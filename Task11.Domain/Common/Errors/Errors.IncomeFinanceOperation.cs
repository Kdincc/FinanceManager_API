using ErrorOr;

namespace Task11.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class IncomeFinanceOperation
        {
            public static Error IncomeFinanceOperationNotFound => Error.NotFound(
                code: "IncomeFinanceOperation.NotFound",
                description: "Income finance operation with that id not found!");

            public static Error IncomeFinanceOperationDateNotFound => Error.NotFound(
                code: "IncomeFinanceOperation.DateNotFound",
                description: "Income finance operation with that date not found!");
        }
    }
}
