namespace Task11.Contracts.ExpenseFinanceOperation
{
    public record UpdateExpenseFinanceOperationRequest(string ExpenseFinanceOperationId,
        string Date,
        string ExpenceTypeId,
        decimal Amount,
        string Name);

}
