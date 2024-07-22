using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.Reports.DailyReport;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Contracts.DailyReport;

namespace Task11.Presentation.Controllers
{
    [Route("reports")]
    public class ReportsController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet("daily")]
        [ProducesResponseType<DailyReport>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDailyReport(GetDailyReportRequest request)
        {
            var query = _mapper.Map<GetDailyReportQuery>(request);

            var result = await _sender.Send(query);

            return result.Match(Ok, Problem);
        }
    }
}
