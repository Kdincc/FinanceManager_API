using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation.Entities;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed class DeleteIncomeTypeCommandHandler(IRepository<IncomeType> repository) : IRequestHandler<DeleteIncomeTypeCommand, ErrorOr<IncomeTypesResult>>
    {
        private readonly IRepository<IncomeType> _repository = repository;

        public async Task<ErrorOr<IncomeTypesResult>> Handle(DeleteIncomeTypeCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync();
        }
    }
}
