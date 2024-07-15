using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.ExpenseTypes;
using Task11.Application.ExpenseTypes.Commands.Create;
using Task11.Application.ExpenseTypes.Queries.GetExpenseTypes;
using Task11.Contracts.ExpenseType;

namespace Task11.Presentation.Controllers
{
    [Route("expenses")]
    public sealed class ExpenseTypesControllercs(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet("all")]
        [ProducesResponseType<IEnumerable<ExpenseTypesResult>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) 
        {
            IEnumerable<ExpenseTypesResult> results = await _sender.Send(new GetExpenseTypesQuery(), cancellationToken);

            return Ok(results);
        }

        [HttpPost("create")]
        [ProducesResponseType<CreateExpenseTypeResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateExpenseType(CreateExpenseTypeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateExpenseTypeCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(Ok, Problem);
        }
    }
}
