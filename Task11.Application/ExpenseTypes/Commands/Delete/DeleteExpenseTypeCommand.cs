using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public record DeleteExpenseTypeCommand(ExpenseTypeId ExpenseTypeId) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
