using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.IncomeFinanceOperations;
using Task11.Application.IncomeFinanceOperations.Commands.Create;
using Task11.Application.IncomeFinanceOperations.Commands.Delete;
using Task11.Application.IncomeFinanceOperations.Commands.Update;
using Task11.Application.IncomeFinanceOperations.Queries.GetIncomeFinanceOperations;
using Task11.Contracts.IncomeFinanceOperation;
using Task11.Presentation.ApiRoutes;

namespace Task11.Presentation.Controllers
{
    public class IncomeFinanceOperationsController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet(Routes.IncomeFinanceOperation.GetAll)]
        [ProducesResponseType<IEnumerable<IncomeFinanceOperationResult>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIncomeFinanceOperations(CancellationToken cancellationToken)
        {
            IEnumerable<IncomeFinanceOperationResult> results = await _sender.Send(new GetIncomeFinanceOperationsQuery(), cancellationToken);

            return Ok(results);
        }

        [HttpPost(Routes.IncomeFinanceOperation.Create)]
        [ProducesResponseType<IncomeFinanceOperationResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateIncomeFinaseOperation(CreateIncomeFinanceOperationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateIncomeFinanceOperationCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpPut(Routes.IncomeFinanceOperation.Update)]
        [ProducesResponseType<IncomeFinanceOperationResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateIncomeFinanceOperation(UpdateIncomeFinanceOperationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateIncomeFinanceOperationCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpDelete(Routes.IncomeFinanceOperation.Delete)]
        [ProducesResponseType<IncomeFinanceOperationResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIncomeFinanceOperation(DeleteIncomeFinanceOperationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteIncomeFinanceOperationCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }
    }
}
