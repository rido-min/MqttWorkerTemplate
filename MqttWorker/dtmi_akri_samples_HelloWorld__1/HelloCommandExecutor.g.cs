/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Akri.Mqtt;
    using Akri.Mqtt.RPC;
    using MQTTnet.Client;
    using MqttWorker;

    public static partial class HelloWorld
    {
        /// <summary>
        /// Specializes a <c>CommandExecutor</c> class for Command 'Hello'.
        /// </summary>
        public class HelloCommandExecutor : CommandExecutor<HelloCommandRequest, HelloCommandResponse>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HelloCommandExecutor"/> class.
            /// </summary>
            internal HelloCommandExecutor(IMqttPubSubClient mqttClient)
                : base(mqttClient, "Hello", new Utf8JsonSerializer())
            {
            }
        }
    }
}
