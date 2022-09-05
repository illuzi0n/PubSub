using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus.BasicEventBus.EventHandler
{
    public class OtherEventHandler : IEventHandler<SomeEvent>
    {
        public OtherEventHandler(Delegate @delegate)
        {
            Delegate = @delegate;
        }

        public Delegate Delegate { get; }

        public void Handle(SomeEvent @event)
        {
            Delegate.DynamicInvoke();
        }
    }
}
