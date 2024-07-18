using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Common.Persistance;
using Task11.Domain.IncomeFinanceOperation;
using Task11.Domain.IncomeFinanceOperation.ValueObjects;
using Task11.Domain.Common.Errors;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    internal class DeleteIncomeFinanceOperationCommandHandler(
        IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> incomeFinanceOperationRepository) : IRequestHandler<DeleteIncomeFinanceOperationCommand, ErrorOr<IncomeFinanceOperationResult>>
    {
        private readonly IRepository<IncomeFinanceOperation, IncomeFinanceOperationId> _incomeFinanceOperationRepository = incomeFinanceOperationRepository;

        public async Task<ErrorOr<IncomeFinanceOperationResult>> Handle(DeleteIncomeFinanceOperationCommand request, CancellationToken cancellationToken)
        {
            IncomeFinanceOperation financeOperation = await _incomeFinanceOperationRepository.GetByIdAsync(request.IncomeFinanceOperationId, cancellationToken);

            if (financeOperation is null)
            {
                return Errors.IncomeFinanceOperation.IncomeFinanceOperationNotFound;
            }

            await _incomeFinanceOperationRepository.DeleteAsync(financeOperation, cancellationToken);

            return new IncomeFinanceOperationResult(financeOperation);
        }
    }
}
