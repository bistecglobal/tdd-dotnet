using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddSample.Api.Application.Query;
using TddSample.Api.Application.Repository;
using TddSample.Api.Application.Services;
using TddSample.Api.Controllers;
using TddSample.Domain;
using Xunit;

namespace TddSamples.Api.Tests
{
    public class SlotControlerTests
    {
        private readonly Mock<ISlotRepository> _repositoryMock;
        private readonly Mock<IDateTimeService> _dateTimeServiceMock;
        private IMediator? _mediator;


        public SlotControlerTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMediatR(typeof(GetSlotsQuery));
            _repositoryMock = new Mock<ISlotRepository>();
            _dateTimeServiceMock = new Mock<IDateTimeService>();
            serviceCollection.AddSingleton(_repositoryMock.Object);
            serviceCollection.AddSingleton(_dateTimeServiceMock.Object);
            _mediator = serviceCollection.BuildServiceProvider().GetService<IMediator>();
        }

        [Fact]
        public async Task ShouldCallHandlerAsync()
        {
            // Arange 
            var controller = new SlotsController(_mediator);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(
                new List<Slot>
                {
                    new Slot { From = new System.TimeSpan(18,0,0)}
                });

            // Act
            var result = await controller.Get();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var slots = Assert.IsAssignableFrom<IEnumerable<Slot>>(objectResult.Value);

            slots.First().From.Hours.Should().Be(18);

        }

        [Theory(DisplayName = "Should Return Slots After Current Time")]
        [InlineData(17, 3)]
        [InlineData(18, 2)]
        [InlineData(21, 0)]
        public async Task ShouldReturnSlotsAfterCurrentTime(int currentHour, int expectedCount)
        {
            // Arange 
            var controller = new SlotsController(_mediator);
            _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(
               new List<Slot>
               {
                    new Slot { From = new System.TimeSpan(16,0,0)},
                    new Slot { From = new System.TimeSpan(17,0,0)},
                    new Slot { From = new System.TimeSpan(18,0,0)},
                    new Slot { From = new System.TimeSpan(19,0,0)},
                    new Slot { From = new System.TimeSpan(20,0,0)}
               });

            _dateTimeServiceMock.Setup(x => x.Current()).Returns(new System.DateTime(2022, 01, 13, currentHour, 0, 0));

            // Act
            var result = await controller.Get();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var slots = Assert.IsAssignableFrom<IEnumerable<Slot>>(objectResult.Value);

            slots.Should().HaveCount(expectedCount);

        }
    }
}