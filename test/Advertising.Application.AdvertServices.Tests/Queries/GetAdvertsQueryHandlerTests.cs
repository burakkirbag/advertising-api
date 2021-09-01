using Advertising.Adverts;
using Advertising.Adverts.Queries.GetAdverts;
using Advertising.Test.Core;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Advertising.Application.AdvertServices.Tests.Queries
{
    public class GetAdvertsQueryHandlerTests : UnitTestBase
    {
        [Fact]
        public async Task Get_Adverts_Should_Be_Success()
        {
            int pageSize = 10;
            int page = 1;
            string sort = "km";
            string sortType = "asc";
            int categoryId = Faker.Random.Int(min: 1);
            int advertId = Faker.Random.Int(min: 1);
            decimal? minPrice = null;
            decimal? maxPrice = null;
            string fuel = "";
            string gear = "";

            var query = new GetAdvertsQuery(categoryId, minPrice, maxPrice, gear, fuel, page, pageSize, sort, sortType);

            var mapper = new Mock<IMapper>();
            var advertRepository = new Mock<IAdvertRepository>();

            var handler = new GetAdvertsQueryHandler(mapper.Object, advertRepository.Object);

            var exception = await Record.ExceptionAsync(async () => await handler.Handle(query, new CancellationToken()));

            exception.Should().BeNull();
        }
    }
}

