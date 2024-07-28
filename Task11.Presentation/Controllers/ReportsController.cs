using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.Reports.DailyReport;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Application.Reports.PeriodReport;
using Task11.Application.Reports.PeriodReport.Queries;
using Task11.Presentation.ApiRoutes;

namespace Task11.Presentation.Controllers
{
    [Route("reports")]
    public class ReportsController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet(Routes.Reports.GetDailyReport)]
        [ProducesResponseType<DailyReport>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDailyReport()
        {
            var query = new GetDailyReportQuery(date);

            var result = await _sender.Send(query);

            return result.Match(Ok, Problem);
        }

        [HttpGet(Routes.Reports.GetPeriodReport)]
        [ProducesResponseType<PeriodReport>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPeriodReport()
        {
            var query = new GetPeriodReportQuery(startDate, endDate);

            var result = await _sender.Send(query);

            return result.Match(Ok, Problem);
        }
    }
}
