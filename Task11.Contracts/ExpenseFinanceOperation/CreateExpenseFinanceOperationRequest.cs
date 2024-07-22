namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record CreateExpenseFinanceOperationRequest(DateOnly Date, string ExpenseTypeId, decimal Amount, string Name);
}
