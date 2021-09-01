using Advertising.Adverts.Commands.AddVisit;
using Advertising.Bus;
using Advertising.Test.Core;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Advertising.Application.AdvertServices.Tests.Commands
{
    public class AddVisitCommandHandlerTests : UnitTestBase
    {
        [Fact]
        public async Task Visit_Should_Be_Added()
        {
            var advertId = Faker.Random.Int(min: 1);
            var ipAddress = Faker.Random.AlphaNumeric(10);
            var date = DateTime.Now;

            var command = new AddVisitCommand(advertId, ipAddress, date);

            var busService = new Mock<IBusService>();

            var handler = new AddVisitCommandHandler(busService.Object);

            var exception = await Record.ExceptionAsync(async () => await handler.Handle(command, new CancellationToken()));

            exception.Should().BeNull();
        }
    }
}
