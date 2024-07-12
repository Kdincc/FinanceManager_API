using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.IncomeTypes;
using Task11.Application.IncomeTypes.Queries.GetIncomeTypes;
using static Task11.Domain.Common.Errors.Errors;

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
    }
}
