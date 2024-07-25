/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class HelloCommandRequest
    {
        /// <summary>
        /// The Command request argument.
        /// </summary>
        [JsonPropertyName("helloRequest")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string HelloRequest { get; set; } = default!;

    }
}
