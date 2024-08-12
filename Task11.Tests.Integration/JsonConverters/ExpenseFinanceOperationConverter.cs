using System.Text.Json;
using System.Text.Json.Serialization;
using Task11.Application.ExpenseFinanceOperations;
using Task11.Domain.Common.ValueObjects;
using Task11.Domain.ExpenseFinanceOperationAggregate;
using Task11.Domain.ExpenseFinanceOperationAggregate.ValueObjects;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Tests.Integration.JsonConverters
{
    public class ExpenseFinanceOperationConverter : JsonConverter<ExpenseFinanceOperationResult>
    {
        public override ExpenseFinanceOperationResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            var expenseFinanceOperationElement = root.GetProperty("expenseFinanceOperation");

            var idElement = expenseFinanceOperationElement.GetProperty("id");
            var id = idElement.GetProperty("value").GetGuid();

            var amountElement = expenseFinanceOperationElement.GetProperty("amount");
            var amount = amountElement.GetProperty("value").GetDecimal();

            var expenseTypeIdElement = expenseFinanceOperationElement.GetProperty("expenseTypeId");
            var expenseTypeId = expenseTypeIdElement.GetProperty("value").GetGuid();

            var date = expenseFinanceOperationElement.GetProperty("date").GetDateTime();

            var name = expenseFinanceOperationElement.GetProperty("name").GetString();

            var expenseFinanceOperation = new ExpenseFinanceOperation(
                ExpenseFinanceOperationId.Create(id.ToString()),
                DateOnly.FromDateTime(date),
                ExpenseTypeId.Create(expenseTypeId.ToString()),
                Amount.Create(amount),
                name);

            return new ExpenseFinanceOperationResult(expenseFinanceOperation);
        }

        public override void Write(Utf8JsonWriter writer, ExpenseFinanceOperationResult value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ExpenseFinanceOperation, options);
        }
    }
}
