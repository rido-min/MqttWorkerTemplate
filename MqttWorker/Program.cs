using MqttWorker;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddSingleton(MqttClientFactoryProvider.MqttClientFactory)
    .AddTransient<ProducerService>()
    .AddTransient<ConsumerService>()
    .AddHostedService<Worker>();

IHost host = builder.Build();
host.Run();
