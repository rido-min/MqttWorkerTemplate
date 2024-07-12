# MqttWorkerTemplate

dotnet template to create a Worker project ready to Pub/Sub to the MQ broker in AIO

## Features

- Configure .NET DI with
- - A MqttSessionClient as a Singleton
- - Pub/Sub binders using the MqttClient
- - A Hosted service to initialize the MqttClient
- A `deployment.yaml` file to easily deploy into a AIO cluster
- - Configuring the CAFile to trust the TLS connection
- - Configuring the SAT token to authenticate with K8s tokens
- LunchSettings to run/debug from an IDE
- Logging Settings
- - With support to log all MQTT traffic


## How to install

```
dotnet new install <Path-To-Rido.MqttWorker.nupkg>
```

## How to use

```
dotnet new mqttworker -o MyMqttWorker
```

## Project files

```
MyMqttWorker
│   appsettings.Development.json
│   appsettings.json
│   build.cmd
│   ConsumerService.cs
│   deployment.yaml
│   MqttClientFactoryProvider.cs
│   MyMqttWorker.csproj
│   nuget.config
│   ProducerService.cs
│   Program.cs
│   Worker.cs
└───Properties
        launchSettings.json
```