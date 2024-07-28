using Mapster;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Application.Reports.PeriodReport.Queries;
using Task11.Contracts.Reports;

namespace Task11.Presentation.Mappings
{
    public sealed class ReportsMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetDailyReportRequest, GetDailyReportQuery>();

            config.NewConfig<GetPeriodReportRequest, GetPeriodReportQuery>();
        }
    }
}
