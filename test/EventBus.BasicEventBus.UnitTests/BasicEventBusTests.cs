using EventBus.BasicEventBus.EventHandler;
using EventBus.Events;
using NUnit.Framework;

namespace EventBus.BasicEventBus.UnitTests
{
    public class BasicEventBusTests
    {
        private BasicEventBus? _eventBus;
        private SomeEvent? _someEvent;
        private string? _eventDescription;

        [SetUp]
        public void Setup()
        {
            _eventBus = new BasicEventBus();
            _eventDescription = "some event has ocurred";
            _someEvent = new SomeEvent(_eventDescription);
        }

        [Test]
        public void GivenEventWasSubscribedWhenEventIsPublishedThenEventIsHandled()
        {
            // arrange
            var expectedEventHandledCount = 1;
            var eventHandledCount = 0;
            string eventDescription = string.Empty;

            var eventHandler = new SomeEventHandler((string jobDescription) =>
            {
                eventHandledCount++;
                eventDescription = jobDescription;
            });
            _eventBus!.Subscribe<SomeEvent>(eventHandler);

            // act
            _eventBus.Publish(_someEvent!);

            // assert
            Assert.That(eventHandledCount, Is.EqualTo(expectedEventHandledCount));
            Assert.That(eventDescription, Is.EqualTo(_eventDescription));
        }

        [Test]
        public void GivenEventWasSubscribedByTwoEventHandlersWhenEventIsPublishedThenEventIsHandledByBothEventHandlers()
        {
            // arrange
            var expectedSomeEventHandledCount = 1;
            var eventSomeHandledCount = 0;

            var expectedSomeOtherHandledCount = 1;
            var eventOtherHandledCount = 0;
            string eventDescription = string.Empty;

            var someEventHandler = new SomeEventHandler((string jobDescription) => 
            {
                eventSomeHandledCount++;
                eventDescription = jobDescription;
            });
            var otherEventHander = new OtherEventHandler(() => eventOtherHandledCount++);

            _eventBus!.Subscribe<SomeEvent>(someEventHandler);
            _eventBus!.Subscribe<SomeEvent>(otherEventHander);

            // act
            _eventBus.Publish(_someEvent!);

            // assert
            Assert.That(eventSomeHandledCount, Is.EqualTo(expectedSomeEventHandledCount));
            Assert.That(eventOtherHandledCount, Is.EqualTo(expectedSomeOtherHandledCount));
            Assert.That(eventDescription, Is.EqualTo(_eventDescription));
        }

        [Test]
        public void GivenEventWasNotSubscribedWhenEventIsPublishedThenEventIsNotHandled()
        {
            // arrange
            var expectedSomeEventHandledCount = 0;
            var eventSomeHandledCount = 0;

            var expectedSomeOtherHandledCount = 0;
            var eventOtherHandledCount = 0;

            var someEventHandler = new SomeEventHandler(() => eventSomeHandledCount++);
            var otherEventHander = new OtherEventHandler(() => eventOtherHandledCount++);

            _eventBus!.Subscribe<SomeEvent>(someEventHandler);
            _eventBus!.Subscribe<SomeEvent>(otherEventHander);

            var anotherEvent = new AnotherEvent();

            // act
            _eventBus!.Publish(anotherEvent!);

            // assert
            Assert.That(eventSomeHandledCount, Is.EqualTo(expectedSomeEventHandledCount));
            Assert.That(eventOtherHandledCount, Is.EqualTo(expectedSomeOtherHandledCount));
        }

        [Test]
        public void GivenEventWasUnsubscribedWhenEventIsPublishedThenEventIsNotHandled()
        {
            // arrange
            var expectedEventHandledCount = 0;
            var eventHandledCount = 0;

            var eventHandler = new SomeEventHandler(() => eventHandledCount++);
            _eventBus!.Subscribe<SomeEvent>(eventHandler);
            _eventBus!.Unsubscribe<SomeEvent>(eventHandler);

            // act
            _eventBus.Publish(_someEvent!);

            // assert
            Assert.That(eventHandledCount, Is.EqualTo(expectedEventHandledCount));
        }
    }
}