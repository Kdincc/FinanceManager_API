using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    public record DeleteIncomeFinanceOperationCommand(string IncomeFinanceOperationId) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
