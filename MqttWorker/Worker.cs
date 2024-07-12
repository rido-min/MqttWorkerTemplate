using Akri.Mqtt.Connection;
using Akri.Mqtt.Session;
using MQTTnet.Client;

namespace MqttWorker;

public class Worker(MqttSessionClient mqttClient, IServiceProvider provider, ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
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
        MqttConnectionSettings mcs = MqttConnectionSettings.FromConnectionString(configuration.GetConnectionString("Default")!);
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
