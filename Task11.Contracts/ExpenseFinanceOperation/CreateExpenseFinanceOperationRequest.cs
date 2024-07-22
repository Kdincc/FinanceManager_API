namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record CreateExpenseFinanceOperationRequest(string Date, string ExpenseTypeId, decimal Amount, string Name);
}
