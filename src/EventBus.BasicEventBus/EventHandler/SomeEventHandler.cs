using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus.BasicEventBus.EventHandler
{
    public class SomeEventHandler : IEventHandler<SomeEvent>
    {
        public SomeEventHandler(Delegate @delegate)
        {
            Delegate = @delegate;
        }

        public Delegate Delegate { get; }

        public void Handle(SomeEvent @event)
        {
            Delegate.DynamicInvoke(@event.NewJobDescription);
        }
    }
}
