using MqttWorker;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddSingleton(MqttSessionClientFactoryProvider.MqttSessionClientFactory)
    .AddTransient<HelloWorldClient>()
    .AddTransient<HelloWorldService>()
    .AddHostedService<Worker>();

IHost host = builder.Build();
host.Run();
