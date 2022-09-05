using API.Abstractions;
using API.Dtos;
using EventBus.Abstractions;
using EventBus.Events;

namespace API.Services
{
    public class SomeService : ISomeService
    {
        private readonly IEventBus _eventBus;

        public SomeService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Create(SomeDto someDto)
        {
            if (someDto.FirstName is null || someDto.FirstName == string.Empty)
            {
                return;
            }

            if (someDto.LastName is null || someDto.LastName == string.Empty)
            {
                return;
            }

            if (someDto.JobName is null || someDto.JobName == string.Empty)
            {
                return;
            }

            var newJobDescription = $"{someDto.FirstName} {someDto.LastName} is now a {someDto.JobName}";
            _eventBus.Publish(new SomeEvent(newJobDescription));
        }
    }
}
