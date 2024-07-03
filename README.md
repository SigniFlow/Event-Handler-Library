# Event-Handler-Library
A library for creating SigniFlow Event Handlers

[![Nuget](https://img.shields.io/nuget/v/SigniFlow.EventHandler) ![Nuget](https://img.shields.io/nuget/dt/SigniFlow.EventHandler)](https://www.nuget.org/packages/SigniFlow.EventHandler/)

To learn what an Event Handler is, please consult our [Event Handler product page](https://www.signiflow.com/connect-with-eventhandler/)

## Usage

This library supports [.NET 6 Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0):

```CSharp
// Setup WebApp builder
var builder = WebApplication.CreateBuilder(args);

var myAuthOptions = new EventHandlerAuthOptions(signiFlowSecret: "myEventHandlerSecret"); /// Your secret as set up in your business config in SigniFlow
builder.Services.SetupEventHandler<MyEventHandlerImpl>(myAuthOptions); // You'll need to implement your own IEventHandler

// Build WebApp
var app = builder.Build();

// Register event handler API
app.UseEventHandler("/path/to/receiver");

// Run WebApp
app.Run();
```