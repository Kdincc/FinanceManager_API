using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    public record DeleteIncomeFinanceOperationCommand(IncomeFinanceOperationId IncomeFinanceOperationId) : IRequest<ErrorOr<IncomeFinanceOperationResult>>;
}
