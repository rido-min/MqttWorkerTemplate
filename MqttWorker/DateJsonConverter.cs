namespace MqttWorker
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Class for customized JSON conversion of <c>DateOnly</c> values to/from string representations in ISO 8601 Date format.
    /// </summary>
    internal sealed class DateJsonConverter : JsonConverter<DateOnly>
    {
        /// <inheritdoc/>
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.Parse(reader.GetString()!, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("o", CultureInfo.InvariantCulture));
        }
    }
}
