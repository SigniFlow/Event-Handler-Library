using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SigniFlow.EventHandler.ConfigurationModels;
using SigniFlow.EventHandler.HttpModels;
namespace SigniFlow.EventHandler.Api;

public static class EventHandlerApi
{
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
    /// <param name="eventHandler">An Event Handler, typically supplied by DI</param>
    /// <param name="authOptions">Auth Options for the Event Handler, typically supplied by DI</param>
    /// <param name="signiFlowEvent">The Event to Handle</param>
    /// <returns></returns>
    public static async Task<string> HandleEvent(IEventHandler eventHandler, EventHandlerAuthOptions authOptions,
        SigniFlowEvent signiFlowEvent)
    {
        return (await EventDelegatorFactory
            .GetEventDelegator(signiFlowEvent, eventHandler, authOptions)
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

    /// <summary>
    /// Adds an Event Handler at the specified endpoint
    /// </summary>
    /// <param name="app">The application to host the webapp on</param>
    /// <param name="route">The route to the endpoint on which the event handler will run</param>
    public static void UseEventHandler(this WebApplication app, string route)
    {
        app.MapPost(route, Handler);
    }

    private static async Task<string> Handler([FromServices] IEventHandler eventHandler,
        [FromServices] EventHandlerAuthOptions authOptions, HttpContext ctx)
    {
        if (!ctx.Request.HasFormContentType)
        {
            return "Err:Invalid";
        }
        var form = await ctx.Request.ReadFormAsync();
        var parsedDate = DateTime.TryParse(form["ED"], out var eventDate);
        var signiFlowEvent = new SigniFlowEvent(form["SFS"], form["ET"], form["DI"], form["UI"], form["UE"],
            parsedDate ? eventDate : DateTime.UtcNow, form["FI"], form["FN"], form["ADF"]);
        return await EventHandlerApi.HandleEvent(eventHandler, authOptions, signiFlowEvent);
    }
}
