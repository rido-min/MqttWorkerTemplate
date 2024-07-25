using Akri.Mqtt.Connection;
using Akri.Mqtt.Session;
using MQTTnet.Client;
using MqttWorker.dtmi_akri_samples_HelloWorld__1;

namespace MqttWorker;

public class Worker(MqttSessionClient mqttClient, IServiceProvider provider, ILogger<Worker> logger, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectAsync(stoppingToken);

        HelloWorldClient client = provider.GetService<HelloWorldClient>()!;
        await client.StartAsync(stoppingToken);

        HelloWorldService service = provider.GetService<HelloWorldService>()!;
        await service.StartAsync(null, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await client.HelloAsync(mqttClient.ClientId, new HelloCommandRequest { HelloRequest = "World" }, null, null, stoppingToken);
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
