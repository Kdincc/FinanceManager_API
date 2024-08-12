using System.Text.Json;
using System.Text.Json.Serialization;
using Task11.Application.IncomeFinanceOperations;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.IncomeFinanceOperationAggregate;
using Task11.Domain.IncomeFinanceOperationAggregate.ValueObjects;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Tests.Integration.JsonConverters
{
    public class IncomeFinanceOperationResultConverter : JsonConverter<IncomeFinanceOperationResult>
    {
        public override IncomeFinanceOperationResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            var incomeFinanceOperationElement = root.GetProperty("incomeFinanceOperation");

            var idElement = incomeFinanceOperationElement.GetProperty("id");
            var id = idElement.GetProperty("value").GetGuid();

            var name = incomeFinanceOperationElement.GetProperty("name").GetString();

            var amountElement = incomeFinanceOperationElement.GetProperty("amount");
            var amount = amountElement.GetProperty("value").GetDecimal();

            var incomeTypeIdElement = incomeFinanceOperationElement.GetProperty("incomeTypeId");
            var incomeTypeId = incomeTypeIdElement.GetProperty("value").GetGuid();

            var date = incomeFinanceOperationElement.GetProperty("date").GetDateTime();

            var incomeFinanceOperation = new IncomeFinanceOperation(
                IncomeFinanceOperationId.Create(id.ToString()),
                DateOnly.FromDateTime(date),
                IncomeTypeId.Create(incomeTypeId.ToString()),
                Amount.Create(amount),
                name);

            return new IncomeFinanceOperationResult(incomeFinanceOperation);
        }

        public override void Write(Utf8JsonWriter writer, IncomeFinanceOperationResult value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.IncomeFinanceOperation, options);
        }
    }
}
