using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.ExpenseFinanceOperations;
using Task11.Application.ExpenseFinanceOperations.Queries.GetExpenceFinanceOperations;

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
    }
}
