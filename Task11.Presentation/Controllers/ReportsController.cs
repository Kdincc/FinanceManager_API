using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.Reports.DailyReport;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Application.Reports.PeriodReport;
using Task11.Application.Reports.PeriodReport.Queries;

namespace Task11.Presentation.Controllers
{
    [Route("reports")]
    public class ReportsController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet("daily/{date}")]
        [ProducesResponseType<DailyReport>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDailyReport(string date)
        {
            var query = new GetDailyReportQuery(date);

            var result = await _sender.Send(query);

            return result.Match(Ok, Problem);
        }

        [HttpGet("period/{startDate}/{endDate}")]
        [ProducesResponseType<PeriodReport>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPeriodReport(string startDate, string endDate)
        {
            var query = new GetPeriodReportQuery(startDate, endDate);

            var result = await _sender.Send(query);

            return result.Match(Ok, Problem);
        }
    }
}
