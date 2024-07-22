﻿using ErrorOr;
using MediatR;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Create
{
    public record CreateExpenseFinanaceOperationCommand(
        string Date,
        string ExpenseTypeId,
        decimal Amount,
        string Name) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
