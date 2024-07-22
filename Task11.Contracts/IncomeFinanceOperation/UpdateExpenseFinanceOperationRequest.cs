namespace Task11.Contracts.IncomeFinanceOperation
{
    public record UpdateIncomeFinanceOperationRequest(string IncomeFinanceOperationId, string Name, string IncomeTypeId, decimal Amount, string Date);

}
