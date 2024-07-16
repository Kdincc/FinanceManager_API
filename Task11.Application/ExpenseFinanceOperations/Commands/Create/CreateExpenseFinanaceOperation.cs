using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Create
{
    public record CreateExpenseFinanaceOperation(
        DateTime Date,
        ExpenseTypeId ExpenseTypeId,
        Amount Amount,
        string Name) : IRequest<ErrorOr<ExpenseFinanceOperationResult>>;
}
