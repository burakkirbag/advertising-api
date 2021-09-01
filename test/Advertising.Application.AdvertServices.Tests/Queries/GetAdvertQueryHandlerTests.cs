using Advertising.Adverts;
using Advertising.Adverts.Queries.GetAdvert;
using Advertising.Test.Core;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Advertising.Application.AdvertServices.Tests.Queries
{
    public class GetAdvertQueryHandlerTests : UnitTestBase
    {
        [Fact]
        public async Task Get_Advert_Should_Be_Success()
        {
            var advertId = Faker.Random.Int(min: 1);

            var query = new GetAdvertQuery(advertId);

            var mapper = new Mock<IMapper>();
            var advertRepository = new Mock<IAdvertRepository>();

            var handler = new GetAdvertQueryHandler(mapper.Object, advertRepository.Object);

            var exception = await Record.ExceptionAsync(async () => await handler.Handle(query, new CancellationToken()));

            exception.Should().BeNull();
        }
    }
}

