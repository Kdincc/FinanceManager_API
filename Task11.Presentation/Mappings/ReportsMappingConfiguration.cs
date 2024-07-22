using Mapster;
using Task11.Application.Reports.DailyReport.Queries;
using Task11.Contracts.DailyReport;

namespace Task11.Presentation.Mappings
{
    public sealed class ReportsMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetDailyReportRequest, GetDailyReportQuery>()
                .Map(dest => dest.Date, src => DateOnly.Parse(src.Date));
        }
    }
}
