namespace EventBus.Abstractions
{
    public interface IEventHandler<in TIEvent> : IEventHandler
        where TIEvent : IEvent
    {
        void Handle(TIEvent @event);
    }

    public interface IEventHandler
    {
    }
}
