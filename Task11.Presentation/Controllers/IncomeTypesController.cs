using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.IncomeTypes;
using Task11.Application.IncomeTypes.Commands.Create;
using Task11.Application.IncomeTypes.Queries.GetIncomeTypes;
using Task11.Contracts.IncomeType;

namespace Task11.Presentation.Controllers
{
    [Route("incomeTypes")]
    public class IncomeTypesController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet("all")]
        [ProducesResponseType<IEnumerable<IncomeTypesResult>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIncomeTypes(CancellationToken cancellationToken) 
        {
            IEnumerable<IncomeTypesResult> incomeTypes = await _sender.Send(new GetIncomeTypesQuery(), cancellationToken);

            return Ok(incomeTypes);
        }

        [HttpPost("create")]
        [ProducesResponseType<IncomeTypesResult>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateIncomeType(CreateIncomeTypeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateIncomeTypeCommand>(request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(
                Ok,
                Problem);
        }

    }
}
