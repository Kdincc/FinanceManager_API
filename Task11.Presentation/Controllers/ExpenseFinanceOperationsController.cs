using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.ExpenseFinanceOperations;
using Task11.Application.ExpenseFinanceOperations.Commands.Create;
using Task11.Application.ExpenseFinanceOperations.Commands.Delete;
using Task11.Application.ExpenseFinanceOperations.Commands.Update;
using Task11.Application.ExpenseFinanceOperations.Queries.GetExpenceFinanceOperations;
using Task11.Contracts.ExpenseFinanceOperation;

namespace Task11.Presentation.Controllers
{
    [Route("expenses/operations")]
    public class ExpenseFinanceOperationsController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet("all")]
        [ProducesResponseType<IEnumerable<ExpenseFinanceOperationResult>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExpenseFinanceOperations(CancellationToken cancellationToken)
        {
            IEnumerable<ExpenseFinanceOperationResult> results = await _sender.Send(new GetExpenceFinanceOperationsQuery(), cancellationToken);

            return Ok(results);
        }

        [HttpPost("create")]
        [ProducesResponseType<ExpenseFinanceOperationResult>(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateExpenseFinaseOperation(CreateExpenseFinanceOperationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateExpenseFinanaceOperationCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("update")]
        [ProducesResponseType<ExpenseFinanceOperationResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExpenseFinanceOperation(UpdateExpenseFinanceOperationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateExpenceFinanceOperationCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpDelete("delete")]
        [ProducesResponseType<ExpenseFinanceOperationResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExpenseFinanceOperation(DeleteExpenseFinanceOperationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteExpenceFinanseOperationCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }
    }
}
