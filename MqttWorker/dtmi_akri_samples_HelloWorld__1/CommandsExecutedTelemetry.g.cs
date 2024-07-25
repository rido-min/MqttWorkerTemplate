/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class CommandsExecutedTelemetry
    {
        /// <summary>
        /// The 'commandsExecuted' Telemetry.
        /// </summary>
        [JsonPropertyName("commandsExecuted")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? CommandsExecuted { get; set; } = default;

    }
}
