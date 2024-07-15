using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.ExpenseTypes.Commands.Create
{
    public record CreateExpenseTypeCommand(string Name, string Description) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
