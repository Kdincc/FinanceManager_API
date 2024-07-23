﻿using ErrorOr;
using MediatR;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public record DeleteExpenceFinanseOperationCommand(string Id) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
