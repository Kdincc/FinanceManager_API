using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Task11.Presentation.Controllers
{
    [Route("incomeTypes")]
    public class IncomeTypesController(ISender sender) : ApiController
    {
        private readonly ISender _sender = sender;

        [HttpGet("all")]
        public IActionResult GetIncomeTypes() 
        {


            return Ok();
        }
    }
}
