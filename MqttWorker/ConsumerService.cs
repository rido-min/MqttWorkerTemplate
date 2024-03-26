using MQTTnet;
using MQTTnet.Client;

namespace MqttWorker;

internal class ConsumerService(IMqttClient mqttClient)
{
    internal Action<string>? OnMessageReceived { get; set; }

    internal async Task StartAsync(string topic, CancellationToken cancellationToken = default)
    {
        mqttClient.ApplicationMessageReceivedAsync += msg =>
        {
            OnMessageReceived?.Invoke(msg.ApplicationMessage.ConvertPayloadToString());
            return Task.CompletedTask;
        };
        await mqttClient.SubscribeAsync(topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce, cancellationToken);
    }
}
