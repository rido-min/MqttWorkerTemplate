/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using System;
    using Akri.Mqtt;
    using Akri.Mqtt.Telemetry;
    using MQTTnet.Client;
    using MqttWorker;

    public static partial class HelloWorld
    {
        /// <summary>
        /// Specializes the <c>TelemetryReceiver</c> class for type <c>CommandsExecutedTelemetry</c>.
        /// </summary>
        public class CommandsExecutedTelemetryReceiver : TelemetryReceiver<CommandsExecutedTelemetry>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CommandsExecutedTelemetryReceiver"/> class.
            /// </summary>
            internal CommandsExecutedTelemetryReceiver(IMqttPubSubClient mqttClient)
                : base(mqttClient, "commandsExecuted", new Utf8JsonSerializer())
            {
            }
        }
    }
}
