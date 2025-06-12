using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    public class CollisionEventConverter : System.Text.Json.Serialization.JsonConverter<ICollisionEvent>
    {
        public override ICollisionEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            var type = root.GetProperty("Type").GetString();

            return type switch
            {
                nameof(CollisionEvent) => JsonSerializer.Deserialize<CollisionEvent>(root.GetRawText(), options),
                nameof(WallCollisionEvent) => JsonSerializer.Deserialize<WallCollisionEvent>(root.GetRawText(), options),
                _ => throw new NotSupportedException($"Unknown collision event type: {type}")
            };
        }

        public override void Write(Utf8JsonWriter writer, ICollisionEvent value, JsonSerializerOptions options)
        {
            var type = value.GetType().Name;

            using var jsonDoc = JsonDocument.Parse(JsonSerializer.Serialize(value, value.GetType(), options));
            writer.WriteStartObject();
            writer.WriteString("Type", type);

            foreach (var prop in jsonDoc.RootElement.EnumerateObject())
            {
                prop.WriteTo(writer);
            }

            writer.WriteEndObject();
        }
    }

}
