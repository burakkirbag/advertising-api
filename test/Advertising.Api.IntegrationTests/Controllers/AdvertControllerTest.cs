using Advertising.Api.IntegrationTests.Extensions;
using Advertising.Api.Models.Requests;
using Advertising.Test.Core;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Advertising.Api.IntegrationTests.Controllers
{
    public class AdvertControllerTest : IntegrationTestBase<Startup>
    {
        public AdvertControllerTest() : base()
        {
            Host.Start();
        }

        [Fact]
        public async Task Get_All_Should_Return_Success()
        {
            var request = new GetAdvertsRequest { PageSize = 10, Page = 1, Sort = "km", SortType = "asc" };

            string query = await request.ToQueryStringParameters();

            var response = await Client.GetAsync($"/api/v1/advert/all?{query}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_All_Should_Return_BadRequest()
        {
            var request = new GetAdvertsRequest { PageSize = 10, Page = 1, Sort = "km", SortType = "asc", MinPrice = 9999, MaxPrice = 1000 };

            string query = await request.ToQueryStringParameters();

            var response = await Client.GetAsync($"/api/v1/advert/all?{query}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_All_Should_Return_NoContent()
        {
            var request = new GetAdvertsRequest { PageSize = 10, Page = 1, CategoryId = 9999, Sort = "km", SortType = "asc" };

            string query = await request.ToQueryStringParameters();

            var response = await Client.GetAsync($"/api/v1/advert/all?{query}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Get_Detail_Should_Return_Success()
        {
            var advertId = 1;
            var response = await Client.GetAsync($"/api/v1/advert/get?id={advertId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_Detail_Should_Return_BadRequest()
        {
            var advertId = 0;
            var response = await Client.GetAsync($"/api/v1/advert/get?id={advertId}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Detail_Should_Return_NoContent()
        {
            var advertId = 9999;
            var response = await Client.GetAsync($"/api/v1/advert/get?id={advertId}");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Add_Visit_Should_Return_Success()
        {
            var request = new AddAdvertVisitRequest { AdvertId = 1 };

            var response = await Client.PostAsync($"/api/v1/advert/visit", request.ToJsonStringContent());

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Add_Visit_Should_Return_BadRequest()
        {
            var request = new AddAdvertVisitRequest { AdvertId = 0 };

            var response = await Client.PostAsync($"/api/v1/advert/visit", request.ToJsonStringContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
