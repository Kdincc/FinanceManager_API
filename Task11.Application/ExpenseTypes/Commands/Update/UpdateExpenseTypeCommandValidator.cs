﻿using FluentValidation;
using Task11.Application.Properties;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.ExpenseTypes.Commands.Update
{
    public sealed class UpdateExpenseTypeCommandValidator : AbstractValidator<UpdateExpenseTypeCommand>
    {
        public UpdateExpenseTypeCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxNameLength);

            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxDescriptionLength);

            RuleFor(p => p.Id)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectIdFormatError);
        }
    }
}
