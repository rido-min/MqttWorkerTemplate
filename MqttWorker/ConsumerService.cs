using Akri.Mqtt;
using Akri.Mqtt.Session;
using MQTTnet;
using MQTTnet.Client;

namespace MqttWorker;

internal class ConsumerService(MqttSessionClient mqttClient)
{
    internal Action<string>? OnMessageReceived { get; set; }

    internal async Task StartAsync(string topic, CancellationToken cancellationToken = default)
    {
        mqttClient.ApplicationMessageReceivedAsync += msg =>
        {
            OnMessageReceived?.Invoke(msg.ApplicationMessage.ConvertPayloadToString());
            return Task.CompletedTask;
        };
        await mqttClient.SubscribeAsync(
            new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(new MqttTopicFilterBuilder().WithTopic(topic).Build())
                .Build(), cancellationToken);
    }
}
