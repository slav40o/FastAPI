namespace FastAPI.Layers.Domain.Events;

using FastAPI.Layers.Domain.Events.Abstractions;
using Microsoft.Extensions.DependencyInjection;

using System.Collections.Concurrent;
using System.Reflection;

/// <inheritdoc/>
public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, Func<object, object, Task>> HandlerDelegatesCache = new();

    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public async Task Dispatch(IDomainEvent domainEvent)
    {
        var eventType = domainEvent.GetType();
        var baseHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = this.serviceProvider.GetServices(baseHandlerType);

        foreach (var handler in handlers)
        {
            var concreteHandlerType = handler!.GetType();

            // Retrieve the `MethodInfo` of the method that defines how to expressively call the event handler's `Handle()` method.
            var handleDelegateMethod = GetMethodInfo(nameof(MakeHandleDelegate), BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(eventType, concreteHandlerType);

            var handleDelegate = HandlerDelegatesCache.GetOrAdd(concreteHandlerType, type =>
            {
                // Create a delegate pointing to the `MakeHandleDelegate` method
                var handleDelegateInvoker = handleDelegateMethod.CreateDelegate<Func<Func<object, object, Task>>>();

                // Invokes the `MakeHandleDelegate` method, returning another delegate pointing to the `Handle()` method.
                return handleDelegateInvoker.Invoke();
            });

            // Invokes the `Handle()` method of the handler.
            await handleDelegate(domainEvent, handler);
        }
    }

    private static Func<object, object, Task> MakeHandleDelegate<TEvent, THandler>()
        where TEvent : IDomainEvent
        where THandler : IDomainEventHandler<TEvent>
    {
        return async (evt, handler) =>
        {
            var domainEvent = (TEvent)evt;
            var domainEventHandler = (THandler)handler;

            await domainEventHandler.Handle(domainEvent);
            domainEvent.Handled = true;
        };
    }

    private static MethodInfo GetMethodInfo(string methodName, BindingFlags bindingFlags)
    {
        var methodInfo = typeof(DomainEventDispatcher).GetMethod(methodName, bindingFlags);

        return methodInfo ?? throw new MissingMethodException(methodName);
    }
}
