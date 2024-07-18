namespace Task11.Contracts.IncomeFinanceOperation
{
    public record CreateIncomeFinanceOperationRequest(DateTime Date, string IncomeTypeId, decimal Amount, string Name);
}
