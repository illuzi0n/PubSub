namespace EventBus.Abstractions
{
    public interface IEvent
    {
        public Guid Id { get; }

        public DateTime CreationDate { get; }
    }
}
