# Publish Subscribe pattern

This is a small solution demonstrating the Publish/Subscribe pattern.

The **API** takes input of data, this data is transformed into a new string. This new string is then published to a set of subscribers. The subscribers display the transformed data in the console.

It's possible to implement new EventBus using a broker like RabbitMQ by implementing the IEventBus interface.

## Development Requirements

[Visual Studio 22](https://visualstudio.microsoft.com/downloads)

[.Net SDK 6.0.200](https://dotnet.microsoft.com/download/dotnet/6.0)

## Project structure

- `./src` contains source code of the main project(s)
- `./test` contains all testing project(s)

## Usage

**EventBus** contains abstractions for EventBus implementations

**EventBus.BasicEventBus** is a simple EventBus implementation using C# and delegate

**Inject the event bus in Program.cs**

    builder.Services.AddSingleton<IEventBus, BasicEventBus>();
    
**Register some subscribers**

    var eventBus = app.Services.GetRequiredService<IEventBus>();
    eventBus.Subscribe<SomeEvent>(new SomeEventHandler((string newJobDescription) => Console.WriteLine(newJobDescription)));

**Make a Post to /WeatherForecast**

This will publish the event to it's subscribers.

