/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using Akri.Mqtt;
    using Akri.Mqtt.Telemetry;
    using MQTTnet.Client;
    using MqttWorker;

    public static partial class HelloWorld
    {
        /// <summary>
        /// Specializes the <c>TelemetrySender</c> class for type <c>CommandsExecutedTelemetry</c>.
        /// </summary>
        public class CommandsExecutedTelemetrySender : TelemetrySender<CommandsExecutedTelemetry>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CommandsExecutedTelemetrySender"/> class.
            /// </summary>
            internal CommandsExecutedTelemetrySender(IMqttPubSubClient mqttClient)
                : base(mqttClient, "commandsExecuted", new Utf8JsonSerializer())
            {
            }
        }
    }
}
