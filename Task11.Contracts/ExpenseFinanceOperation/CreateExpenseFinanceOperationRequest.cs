namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record CreateExpenseFinanceOperationRequest(DateTime Date, string ExpenseTypeId, decimal Amount, string Name);
}
