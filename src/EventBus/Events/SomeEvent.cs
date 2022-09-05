using EventBus.Abstractions;

namespace EventBus.Events
{
    public class SomeEvent : IEvent
    {
        public SomeEvent(string newJobDescription)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            NewJobDescription = newJobDescription;
        }

        public Guid Id { get; }

        public DateTime CreationDate { get; }

        public string NewJobDescription { get; }
    }
}
