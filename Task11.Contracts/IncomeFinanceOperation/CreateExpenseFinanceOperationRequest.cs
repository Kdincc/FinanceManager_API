﻿namespace Task11.Contracts.IncomeFinanceOperation
{
    public record CreateIncomeFinanceOperationRequest(DateOnly Date, string IncomeTypeId, decimal Amount, string Name);
}
