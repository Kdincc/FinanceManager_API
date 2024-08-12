using System.Text.Json;
using System.Text.Json.Serialization;
using Task11.Application.ExpenseTypes;
using Task11.Domain.ExpenseType;
using Task11.Domain.ExpenseType.ValueObjects;

namespace Task11.Tests.Integration.JsonConverters
{
    public class ExpenseTypesResultConverter : JsonConverter<ExpenseTypesResult>
    {
        public override ExpenseTypesResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            var expenseTypeElement = root.GetProperty("expenseType");

            var idElement = expenseTypeElement.GetProperty("id");
            var id = idElement.GetProperty("value").GetGuid();

            var name = expenseTypeElement.GetProperty("name").GetString();
            var description = expenseTypeElement.GetProperty("description").GetString();

            var incomeTypeId = ExpenseTypeId.Create(id.ToString());
            var incomeType = new ExpenseType(incomeTypeId, name, description);

            return new ExpenseTypesResult(incomeType);
        }

        public override void Write(Utf8JsonWriter writer, ExpenseTypesResult value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ExpenseType, options);
        }
    }

}
