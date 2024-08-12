using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task11.Application.Reports.DailyReport;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Application.Reports.PeriodReport;
using Task11.Application.Reports.PeriodReport.Queries;
using Task11.Contracts.Reports;
using Task11.Presentation.ApiRoutes;

namespace Task11.Presentation.Controllers
{
    public class ReportsController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet(Routes.Reports.GetDailyReport)]
        [ProducesResponseType<DailyReport>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDailyReport(GetDailyReportRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetDailyReportQuery>(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.Match(Ok, Problem);
        }

        [HttpGet(Routes.Reports.GetPeriodReport)]
        [ProducesResponseType<PeriodReport>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPeriodReport(GetPeriodReportRequest request, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetPeriodReportQuery>(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.Match(Ok, Problem);
        }
    }
}
