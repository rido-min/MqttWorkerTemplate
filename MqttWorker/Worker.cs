using Akri.Mqtt.Connection;
using MQTTnet.Client;

namespace MqttWorker;

public class Worker(IMqttClient mqttClient, IServiceProvider provider, ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectAsync(stoppingToken);

        ConsumerService consumer = provider.GetService<ConsumerService>()!;
        consumer.OnMessageReceived += m => logger.LogInformation("Received message {m}", m);
        await consumer.StartAsync("test/mqtt", stoppingToken);

        ProducerService producer = provider.GetService<ProducerService>()!;

        while (!stoppingToken.IsCancellationRequested)
        {
            await producer.SendMessageAsync("test/mqtt", "hello mqtt");
            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task ConnectAsync(CancellationToken stoppingToken)
    {
        string cs = configuration.GetConnectionString("Default")!;
        MqttConnectionSettings mcs = MqttConnectionSettings.FromConnectionString(cs);
        MqttClientConnectResult connAck = await mqttClient.ConnectAsync(mcs, stoppingToken);

        if (connAck.ResultCode != MqttClientConnectResultCode.Success)
        {
            logger.LogError("Failed to connect to MQTT broker: {connAck.ResultCode}", connAck.ResultCode);
            return;
        }
        else
        {
            logger.LogInformation("Connected with persistent session {c}", connAck.IsSessionPresent);
        }
    }
}
