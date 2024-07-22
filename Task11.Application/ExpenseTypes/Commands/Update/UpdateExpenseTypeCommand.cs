﻿using ErrorOr;
using MediatR;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Application.ExpenseTypes.Commands.Update
{
    public record UpdateExpenseTypeCommand(string ExpenseTypeId, string Name, string Description) : IRequest<ErrorOr<ExpenseTypesResult>>;
}
