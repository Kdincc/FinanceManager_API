namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record UpdateExpenseFinanceOperationRequest(string ExpenseFinanceOperationId, string Name, string ExpenseTypeId, decimal Amount, string Date);

}
