using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TddSample.Api.Controllers;
using TddSample.Domain;
using Xunit;

namespace TddSample.Api.Tests
{
    public class SlotsControllerTests
    {
        [Trait("Type","Slots Controller Tests")]
        [Fact(DisplayName = "Should return slots")]
        public async Task ShouldReturnSlotsAsync()
        {
            // Arrange
            var controller = new SlotsController();
            // Act
            var slots = await controller.Get();
            // Assert
            slots.Should().NotBeNull();
        }

        [Trait("Type", "Slots Controller Tests")]
        [Fact(DisplayName = "Should return valid slots")]
        public async Task ShouldReturnValidSlot()
        {
            // Arrange
            var controller = new SlotsController();
            // Act
            var result = await controller.Get();
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var slots = Assert.IsAssignableFrom<IEnumerable<Slot>>(objectResult.Value);
            // Assert
            slots.First().From.Hours.Should().BeGreaterThan(0);
        }
    }
}