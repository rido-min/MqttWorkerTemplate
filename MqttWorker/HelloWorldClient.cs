using Akri.Mqtt;
using Akri.Mqtt.Session;
using Akri.Mqtt.Telemetry;
using MqttWorker.dtmi_akri_samples_HelloWorld__1;

namespace MqttWorker;

internal class HelloWorldClient(MqttSessionClient mqttClient, ILogger<HelloWorldClient> logger) : HelloWorld.Client(mqttClient)
{
    public override Task ReceiveTelemetry(string senderId, CommandsExecutedTelemetry telemetry, IncomingTelemetryMetadata metadata)
    {
        logger.LogInformation($"Received telemetry from {senderId}: {telemetry.CommandsExecuted}");
        return Task.CompletedTask;
    }
}
