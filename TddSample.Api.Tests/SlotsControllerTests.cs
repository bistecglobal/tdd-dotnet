using FluentAssertions;
using System.Threading.Tasks;
using TddSample.Api.Controllers;
using Xunit;

namespace TddSample.Api.Tests
{
    public class SlotsControllerTests
    {
        [Fact]
        public async Task ShouldReturnSlotsAsync()
        {
            // Arrange
            var controller = new SlotsController();
            // Act
            var slots = await controller.Get();
            // Assert
            slots.Should().NotBeNull();
        }
    }
}