using Microsoft.Extensions.DependencyInjection;
using SigniFlow.EventHandler.ConfigurationModels;
using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler.Api;

public class EventHandlerApi
{
    private readonly IEventHandler _eventHandler;
    private readonly EventHandlerAuthOptions _authOptions;
    public EventHandlerApi(IEventHandler eventHandler, EventHandlerAuthOptions authOptions)
    {
        _eventHandler = eventHandler;
        _authOptions = authOptions;
    }

    /// <summary>
    /// Handles a given <see cref="SigniFlowEvent"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// services.SetupEventHandler&lt;MyEventHandlerImpl&gt;(myAuthOptions);
    /// var eventHandler = new EventHandlerApi();
    /// app.MapPost("/route/to/handler", eventHandler.HandleEvent);
    /// </code>
    /// </example>
    /// <param name="signiFlowEvent"></param>
    /// <returns></returns>
    public async Task<string> HandleEvent(SigniFlowEvent signiFlowEvent)
    {
        return (await EventDelegatorFactory
            .GetEventDelegator(signiFlowEvent, this._eventHandler, this._authOptions)
            .HandleEvent()).ToString();
    }
}
public static class EventHandlerApiExtensions
{
    /// <summary>
    /// Sets up the required DI for the Event Handler
    /// </summary>
    /// <param name="authOptions">An instantiated <see cref="EventHandlerAuthOptions"/> containing the secret for the event handler (from config)</param>
    /// <typeparam name="T">An implementation of <see cref="IEventHandler"/></typeparam>
    public static IServiceCollection SetupEventHandler<T>(this IServiceCollection services,
        EventHandlerAuthOptions authOptions)
        where T : class, IEventHandler
    {
        services.AddScoped<IEventHandler, T>();
        services.AddSingleton<EventHandlerAuthOptions>(provider => authOptions);
        return services;
    }
}
