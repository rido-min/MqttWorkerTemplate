using Akri.Mqtt.Connection;
using Microsoft.Extensions.Logging.Configuration;
using MQTTnet;
using MQTTnet.Client;

namespace MqttWorker;

internal static class MqttClientFactoryProvider
{
    const int maxRetries = 20;
    private static int currentRetries = maxRetries;

    public static Func<IServiceProvider, IMqttClient> MqttClientFactory = service =>
    {
        IConfiguration? config = service.GetService<IConfiguration>();
        bool mqttDiag = config!.GetValue<bool>("mqttDiag");
        IMqttClient mqttClient;
        ILogger logger = LoggerFactory.Create(builder => builder.AddConfiguration(config!)).CreateLogger<Worker>();

        if (mqttDiag)
        {
            //Trace.Listeners.Add(new ConsoleTraceListener());
            mqttClient = new MqttFactory().CreateMqttClient(MqttNetTraceLogger.CreateTraceLogger());
        }
        else
        {
            mqttClient = new MqttFactory().CreateMqttClient();
        }

        mqttClient.DisconnectedAsync += async e =>
        {
            logger.LogWarning("Mqtt Client Disconnected with reason {r}", e.Reason);
            while (currentRetries > 0 && !mqttClient.IsConnected)
            {
                int delay = (maxRetries - currentRetries--) * 1000;
                await Task.Delay(delay);
                logger.LogWarning("Waiting to reconnect " + delay);
                await mqttClient.ReconnectAsync();
                if (mqttClient.IsConnected)
                {
                    logger.LogInformation("Client Reconnected");
                    currentRetries = maxRetries;
                }
            }
        };
        return mqttClient;
    };

}
