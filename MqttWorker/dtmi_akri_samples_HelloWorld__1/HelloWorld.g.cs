/* This is an auto-generated file.  Do not modify. */

#nullable enable

namespace MqttWorker.dtmi_akri_samples_HelloWorld__1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MQTTnet.Client;
    using MQTTnet.Protocol;
    using Akri.Mqtt;
    using Akri.Mqtt.RPC;
    using Akri.Mqtt.Telemetry;
    using MqttWorker;

    [ModelId("dtmi:akri:samples:HelloWorld;1")]
    [CommandTopic("akri/commands/{executorId}/{commandName}")]
    [TelemetryTopic("akri/telemetry-samples/{senderId}/{telemetryName}")]
    public static partial class HelloWorld
    {
        public abstract partial class Service : IAsyncDisposable
        {
            private readonly HelloCommandExecutor helloCommandExecutor;
            private readonly CommandsExecutedTelemetrySender commandsExecutedTelemetrySender;

            public Service(IMqttPubSubClient mqttClient)
            {
                this.CustomTopicTokenMap = new();

                this.helloCommandExecutor = new HelloCommandExecutor(mqttClient) { OnCommandReceived = Hello_Int, CustomTopicTokenMap = this.CustomTopicTokenMap };
                this.commandsExecutedTelemetrySender = new CommandsExecutedTelemetrySender(mqttClient) { CustomTopicTokenMap = this.CustomTopicTokenMap };
            }

            public HelloCommandExecutor HelloCommandExecutor { get => this.helloCommandExecutor; }
            public CommandsExecutedTelemetrySender CommandsExecutedTelemetrySender { get => this.commandsExecutedTelemetrySender; }

            public Dictionary<string, string> CustomTopicTokenMap { get; private init; }

            public abstract Task<ExtendedResponse<HelloCommandResponse>> HelloAsync(HelloCommandRequest request, CommandRequestMetadata requestMetadata, CancellationToken cancellationToken);

            public async Task SendTelemetryAsync(CommandsExecutedTelemetry telemetry, OutgoingTelemetryMetadata metadata, MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtLeastOnce, TimeSpan? messageExpiryInterval = null, CancellationToken cancellationToken = default)
            {
                await this.commandsExecutedTelemetrySender.SendTelemetryAsync(telemetry, metadata, qos, messageExpiryInterval, cancellationToken);
            }

            public async Task StartAsync(int? preferredDispatchConcurrency = null, CancellationToken cancellationToken = default)
            {
                await Task.WhenAll(
                    this.helloCommandExecutor.StartAsync(preferredDispatchConcurrency, cancellationToken)).ConfigureAwait(false);
            }

            public async Task StopAsync(CancellationToken cancellationToken = default)
            {
                await Task.WhenAll(
                    this.helloCommandExecutor.StopAsync(cancellationToken)).ConfigureAwait(false);
            }
            private async Task<ExtendedResponse<HelloCommandResponse>> Hello_Int(ExtendedRequest<HelloCommandRequest> req, CancellationToken cancellationToken)
            {
                ExtendedResponse<HelloCommandResponse> extended = await this.HelloAsync(req.Request!, req.RequestMetadata!, cancellationToken);
                return new ExtendedResponse<HelloCommandResponse> { Response = extended.Response, ResponseMetadata = extended.ResponseMetadata };
            }

            public async ValueTask DisposeAsync()
            {
                await this.helloCommandExecutor.DisposeAsync().ConfigureAwait(false);
            }

            public async ValueTask DisposeAsync(bool disposing)
            {
                await this.helloCommandExecutor.DisposeAsync(disposing).ConfigureAwait(false);
            }
        }

        public abstract partial class Client : IAsyncDisposable
        {
            private readonly HelloCommandInvoker helloCommandInvoker;
            private readonly CommandsExecutedTelemetryReceiver commandsExecutedTelemetryReceiver;

            public Client(IMqttPubSubClient mqttClient)
            {
                this.CustomTopicTokenMap = new();

                this.helloCommandInvoker = new HelloCommandInvoker(mqttClient) { CustomTopicTokenMap = this.CustomTopicTokenMap };
                this.commandsExecutedTelemetryReceiver = new CommandsExecutedTelemetryReceiver(mqttClient) { OnTelemetryReceived = this.ReceiveTelemetry, CustomTopicTokenMap = this.CustomTopicTokenMap };
            }

            public HelloCommandInvoker HelloCommandInvoker { get => this.helloCommandInvoker; }
            public CommandsExecutedTelemetryReceiver CommandsExecutedTelemetryReceiver { get => this.commandsExecutedTelemetryReceiver; }

            public Dictionary<string, string> CustomTopicTokenMap { get; private init; }

            public abstract Task ReceiveTelemetry(string senderId, CommandsExecutedTelemetry telemetry, IncomingTelemetryMetadata metadata);

            public RpcCallAsync<HelloCommandResponse> HelloAsync(string executorId, HelloCommandRequest request, CommandRequestMetadata? requestMetadata = null, TimeSpan? commandTimeout = default, CancellationToken cancellationToken = default)
            {
                CommandRequestMetadata metadata = requestMetadata ?? new CommandRequestMetadata();
                return new RpcCallAsync<HelloCommandResponse>(this.helloCommandInvoker.InvokeCommandAsync(executorId, request, metadata, commandTimeout, cancellationToken), metadata.CorrelationId);
            }

            public async Task StartAsync(CancellationToken cancellationToken = default)
            {
                await Task.WhenAll(
                    this.commandsExecutedTelemetryReceiver.StartAsync(cancellationToken)).ConfigureAwait(false);
            }

            public async Task StopAsync(CancellationToken cancellationToken = default)
            {
                await Task.WhenAll(
                    this.commandsExecutedTelemetryReceiver.StopAsync(cancellationToken)).ConfigureAwait(false);
            }

            public async ValueTask DisposeAsync()
            {
                await this.helloCommandInvoker.DisposeAsync().ConfigureAwait(false);
            }

            public async ValueTask DisposeAsync(bool disposing)
            {
                await this.helloCommandInvoker.DisposeAsync(disposing).ConfigureAwait(false);
            }
        }
    }
}
