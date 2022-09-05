using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        void Subscribe<T>(IEventHandler<T> handler)
            where T : IEvent;

        void Unsubscribe<T>(IEventHandler<T> handler)
            where T : IEvent;

        void Publish(IEvent @event);
    }
}
