using Akri.Mqtt;
using Akri.Mqtt.RPC;
using Akri.Mqtt.Session;
using MqttWorker.dtmi_akri_samples_HelloWorld__1;

namespace MqttWorker;

internal class HelloWorldService(MqttSessionClient mqttClient, ILogger<HelloWorldService> logger) : HelloWorld.Service(mqttClient)
{
    int commandsExecuted = 0;
    public override async Task<ExtendedResponse<HelloCommandResponse>> HelloAsync(HelloCommandRequest request, CommandRequestMetadata requestMetadata, CancellationToken cancellationToken)
    {
        commandsExecuted++;
        await SendTelemetryAsync(new CommandsExecutedTelemetry { CommandsExecuted = commandsExecuted }, null!, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce, null, cancellationToken);
        logger.LogInformation($"Received Hello request: {request.HelloRequest}");
        return new ExtendedResponse<HelloCommandResponse> 
                { 
                    Response = new HelloCommandResponse 
                    { 
                        HelloResponse = $"Hello {request.HelloRequest}!" 
                    } 
            };
    }
}
