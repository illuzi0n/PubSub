using EventBus.Abstractions;

namespace EventBus.Events
{
    public class AnotherEvent : IEvent
    {
        public AnotherEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public DateTime CreationDate { get; }
    }
}
