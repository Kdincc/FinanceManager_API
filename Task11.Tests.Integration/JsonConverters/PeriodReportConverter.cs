using System.Text.Json;
using System.Text.Json.Serialization;
using Task11.Application.Common.DTOs;
using Task11.Application.Reports.PeriodReport;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Tests.Integration.JsonConverters
{
    public class PeriodReportConverter : JsonConverter<PeriodReport>
    {
        public override PeriodReport Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);

            var root = doc.RootElement;

            var datePeriod = root.GetProperty("period");
            var startDate = datePeriod.GetProperty("startDate").GetDateTime();
            var endDate = datePeriod.GetProperty("endDate").GetDateTime();

            var incomeFinanceOperationsElement = root.GetProperty("incomes");
            var incomeFinanceOperations = incomeFinanceOperationsElement.EnumerateArray().Select(x =>
            {
                var id = x.GetProperty("id").GetProperty("value").GetGuid();
                var amount = x.GetProperty("amount").GetProperty("value").GetDecimal();
                var incomeTypeId = x.GetProperty("incomeTypeId").GetProperty("value").GetGuid();
                var date = x.GetProperty("date").GetDateTime();
                var name = x.GetProperty("name").GetString();

                IncomeFinanceOperation incomeFinanceOperation = new IncomeFinanceOperation
                (
                    IncomeFinanceOperationId.Create(id.ToString()),
                    DateOnly.FromDateTime(date),
                    IncomeTypeId.Create(incomeTypeId.ToString()),
                    Amount.Create(amount),
                    name
                );

                return new IncomeFinanceOperationDto(incomeFinanceOperation);
            }).ToList();

            var expenseFinanceOperationsElement = root.GetProperty("expenses");
            var expenseFinanceOperations = expenseFinanceOperationsElement.EnumerateArray().Select(x =>
            {
                var id = x.GetProperty("id").GetProperty("value").GetGuid();
                var amount = x.GetProperty("amount").GetProperty("value").GetDecimal();
                var expenseTypeId = x.GetProperty("expenseTypeId").GetProperty("value").GetGuid();
                var date = x.GetProperty("date").GetDateTime();
                var name = x.GetProperty("name").GetString();

                ExpenseFinanceOperation expenseFinanceOperation = new ExpenseFinanceOperation
                (
                    ExpenseFinanceOperationId.Create(id.ToString()),
                    DateOnly.FromDateTime(date),
                    ExpenseTypeId.Create(expenseTypeId.ToString()),
                    Amount.Create(amount),
                    name
                );

                return new ExpenseFinanceOperationDto(expenseFinanceOperation);
            }).ToList();

            return PeriodReport.Create(
                new DatePeriod(DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate)),
                expenseFinanceOperations,
                incomeFinanceOperations);
        }

        public override void Write(Utf8JsonWriter writer, PeriodReport value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
