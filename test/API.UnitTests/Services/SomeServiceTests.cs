using API.Dtos;
using API.Services;
using EventBus.Abstractions;
using EventBus.Events;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.UnitTests.Services
{
    public class SomeServiceTests
    {
        [Test]
        public void GivenSomeDtoWhenCreateThenEventIsPublished()
        {
            // arrange
            var someDto = new SomeDto
            {
                FirstName = "John",
                LastName = "Lane",
                JobName = "Cashier"
            };
            var eventBusMock = new Mock<IEventBus>();
            var someService = new SomeService(eventBusMock.Object);

            // act
            someService.Create(someDto);

            // assert
            eventBusMock.Verify(m => m.Publish(It.IsAny<IEvent>()), Times.Once());
        }
    }
}
