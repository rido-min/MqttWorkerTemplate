/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using System;
    using System.Collections.Generic;
    using Akri.Mqtt;
    using Akri.Mqtt.RPC;
    using MQTTnet.Client;
    using MqttWorker;

    public static partial class HelloWorld
    {
        /// <summary>
        /// Specializes the <c>CommandInvoker</c> class for Command 'Hello'.
        /// </summary>
        public class HelloCommandInvoker : CommandInvoker<HelloCommandRequest, HelloCommandResponse>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HelloCommandInvoker"/> class.
            /// </summary>
            internal HelloCommandInvoker(IMqttPubSubClient mqttClient)
                : base(mqttClient, "Hello", new Utf8JsonSerializer())
            {
            }
        }
    }
}
