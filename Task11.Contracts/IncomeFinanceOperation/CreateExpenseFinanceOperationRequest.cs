namespace Task11.Contracts.IncomeFinanceOperation
{
    public record CreateIncomeFinanceOperationRequest(string Date, string IncomeTypeId, decimal Amount, string Name);
}
