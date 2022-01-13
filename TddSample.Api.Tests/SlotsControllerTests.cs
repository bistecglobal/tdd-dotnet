using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddSample.Api.Application.Query;
using TddSample.Api.Application.Repository;
using TddSample.Api.Application.Services;
using TddSample.Api.Controllers;
using TddSample.Domain;
using Xunit;

namespace TddSample.Api.Tests
{
    public class SlotsControllerTests
    {
        public IServiceCollection _services;
        public IMediator? Mediator { get; private set; }
        public Mock<ISlotRepository> RepositoryMock { get; private set; }
        public Mock<IDateTimeService> DateTimeMock { get; private set; }

        public SlotsControllerTests()
        {
            DateTimeMock = new Mock<IDateTimeService>();
            RepositoryMock = new Mock<ISlotRepository>();
            _services = new ServiceCollection();
            _services.AddMediatR(typeof(GetSlotsQuery));
            _services.AddSingleton(RepositoryMock.Object);
            _services.AddSingleton(DateTimeMock.Object);
            Mediator = _services.BuildServiceProvider().GetService<IMediator>();
        }

        [Trait("Type","Slots Controller Tests")]
        [Fact(DisplayName = "Should call Repository")]
        public async Task ShouldReturnSlotsAsync()
        {
            // Arrange
            var controller = new SlotsController(Mediator);
            RepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Slot> { new Slot
            {
                From = new TimeSpan(12, 0, 0)
            } }).Verifiable();
            // Act
            var slots = await controller.Get();
            // Assert
            RepositoryMock.Verify();
        }

        [Trait("Type", "Slots Controller Tests")]
        [Fact(DisplayName = "Should return valid slots")]
        public async Task ShouldReturnValidSlot()
        {
            // Arrange
            var controller = new SlotsController(Mediator);
            RepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Slot> { new Slot
            {
                From = new TimeSpan(12, 0, 0)
            } });

            // Act
            var result = await controller.Get();
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var slots = Assert.IsAssignableFrom<IEnumerable<Slot>>(objectResult.Value);
            // Assert
            slots.First().From.Hours.Should().BeGreaterThan(0);
        }

        [Trait("Type", "Slots Controller Tests")]
        [Fact(DisplayName = "Should return slots after current time")]
        public async Task ShouldReturnSlotsAfterCurrentTime()
        {
            // Arrange
            var controller = new SlotsController(Mediator);
            RepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Slot> 
            { 
                new Slot { From = new TimeSpan(1, 0, 0) }, 
                new Slot { From = new TimeSpan(13, 0, 0) },
            });
            var now = new DateTime(2020, 01, 13, 12, 0, 0);
            DateTimeMock.Setup(x => x.Current()).Returns(now);
            // Act
            var result = await controller.Get();
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var slots = Assert.IsAssignableFrom<IEnumerable<Slot>>(objectResult.Value);
            // Assert
            slots.First().From.Hours.Should().BeGreaterThan(now.Hour);
        }

        [Trait("Type", "Slots Controller Tests")]
        [Theory(DisplayName = "Should return correct number of slots after current time")]
        [InlineData(3, 12)]
        [InlineData(2, 13)]
        [InlineData(4, 0)]
        public async Task ShouldReturnNumberofSlotsAfterCurrentTime(int count, int currentHour)
        {
            // Arrange
            var controller = new SlotsController(Mediator);
            RepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Slot>
            {
                new Slot { From = new TimeSpan(1, 0, 0) },
                new Slot { From = new TimeSpan(13, 0, 0) },
                new Slot { From = new TimeSpan(14, 0, 0) },
                new Slot { From = new TimeSpan(15, 0, 0) },
            });
            var now = new DateTime(2020, 01, 13, currentHour, 0, 0);
            DateTimeMock.Setup(x => x.Current()).Returns(now);
            // Act
            var result = await controller.Get();
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var slots = Assert.IsAssignableFrom<IEnumerable<Slot>>(objectResult.Value);
            // Assert
            slots.Count().Should().Be(count);
        }
    }
}