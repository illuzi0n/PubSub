using EventBus.Abstractions;

namespace EventBus.BasicEventBus
{
    public class BasicEventBus : IEventBus
    {
        private readonly Dictionary<string, List<IEventHandler>> _handlers;
        private readonly List<Type> _eventTypes;

        public BasicEventBus()
        {
            _handlers = new Dictionary<string, List<IEventHandler>>();
            _eventTypes = new List<Type>();
        }

        public void Publish(IEvent @event)
        {
            var eventName = @event.GetType().Name;

            if (_handlers.ContainsKey(eventName))
            {
                Type eventType = _eventTypes.Single(t => t.Name == eventName);
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                var handlers = _handlers[eventName];

                foreach (var handler in handlers)
                {
                    _ = concreteType.GetMethod("Handle")?.Invoke(handler, new object[] { @event });
                }
            }
        }

        public void Subscribe<T>(IEventHandler<T> handler)
            where T : IEvent
        {
            var eventName = typeof(T).Name;
            if (_handlers.ContainsKey(eventName))
            {
                _handlers[eventName].Add(handler);
            }
            else
            {
                _handlers.Add(eventName, new List<IEventHandler> { handler });
                _eventTypes.Add(typeof(T));
            }
        }

        public void Unsubscribe<T>(IEventHandler<T> handler)
            where T : IEvent
        {
            var eventName = typeof(T).Name;
            if (_handlers.ContainsKey(eventName) && _handlers[eventName].Contains(handler))
            {
                _handlers[eventName].Remove(handler);

                if (_handlers[eventName].Count == 0)
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.Single(e => e.Name == eventName);
                    _eventTypes.Remove(eventType);
                }
            }
        }
    }
}
