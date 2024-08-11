using System.Text.Json;
using System.Text.Json.Serialization;
using Task11.Application.IncomeTypes;
using Task11.Domain.IncomeType;
using Task11.Domain.IncomeType.ValueObjects;

namespace Task11.Tests.Integration.JsonConverters
{
    public sealed class IncomeTypesResultConverter : JsonConverter<IncomeTypesResult>
    {
        public override IncomeTypesResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            var incomeTypeElement = root.GetProperty("incomeType");

            var idElement = incomeTypeElement.GetProperty("id");
            var id = idElement.GetProperty("value").GetGuid();

            var name = incomeTypeElement.GetProperty("name").GetString();
            var description = incomeTypeElement.GetProperty("description").GetString();

            var incomeTypeId = IncomeTypeId.Create(id.ToString());
            var incomeType = new IncomeType(incomeTypeId, name, description);

            return new IncomeTypesResult(incomeType);
        }

        public override void Write(Utf8JsonWriter writer, IncomeTypesResult value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.IncomeType, options);
        }
    }

}
