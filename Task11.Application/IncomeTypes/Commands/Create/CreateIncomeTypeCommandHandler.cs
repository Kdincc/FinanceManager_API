using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.Entities;

namespace Task11.Application.IncomeTypes.Commands.Create
{
    public sealed class CreateIncomeTypeCommandHandler(IRepository<IncomeType> repository) : IRequestHandler<CreateIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType> _repository = repository;

        public Task<ErrorOr<IncomeTypesResult>> Handle(CreateIncomeTypeCommand request, CancellationToken cancellationToken)
        {

        }
    }
}
