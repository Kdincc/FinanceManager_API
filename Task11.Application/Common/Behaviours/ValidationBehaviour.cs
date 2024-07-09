using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.Common.Behaviours
{
    public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviour(IValidator<TRequest> validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null) 
            {
                return await next();
            }

            ValidationResult validationResult = _validator.Validate(request);

            if (validationResult.IsValid) 
            {
                return await next();
            }

            List<Error> errors = validationResult.Errors.ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage));

            return (dynamic)errors;
        }
    }
}
