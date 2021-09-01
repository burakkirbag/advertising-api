using Advertising.Adverts.Commands.AddVisit;
using Advertising.Adverts.Dto;
using Advertising.Adverts.Queries.GetAdvert;
using Advertising.Adverts.Queries.GetAdverts;
using Advertising.Api.Extensions;
using Advertising.Api.Models.Requests;
using Advertising.Api.Mvc.Controllers;
using Advertising.Api.Mvc.Models;
using Advertising.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Advertising.Api.Controllers
{
    public class AdvertController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AdvertController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(ApiReturn<PagingResultDto<AdvertDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetAdvertsRequest request)
        {
            var result = await _mediator.Send(new GetAdvertsQuery(request.CategoryId, request.MinPrice, request.MaxPrice, request.Gear, request.Fuel, request.Page, request.PageSize, request.Sort, request.SortType));

            if (result.TotalCount > 0)
                return Success($"{result.PageCount} sayfada, {result.TotalCount} adet reklam bulundu.", "", result);
            else
                return NoContent("Belirtmiş olduğunuz kriterlere uygun reklam bulunamadı.", "");
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(ApiReturn<PagingResultDto<AdvertDetailDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDetail([FromQuery] int id)
        {
            if (id == default(int))
                return BadRequest("Görüntülemek istediğiniz reklamın numarasını belirtmelisiniz.", "id alanının doldurulması zorunludur.");

            var result = await _mediator.Send(new GetAdvertQuery(id));
            if (result == null)
                return NoContent("Belirtmiş olduğunuz kriterlere uygun reklam bulunamadı.", "");

            return Success($"{id} numaralı reklamı görüntülemektesiniz.", $"", result);
        }

        [HttpPost]
        [Route("visit")]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiReturn), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddVisit([FromBody] AddAdvertVisitRequest request)
        {
            await _mediator.Send(new AddVisitCommand(request.AdvertId, HttpContext.GetClientIP(), System.DateTime.Now));

            return Created($"Reklam ziyaret bilgisi eklendi.", $"Reklam ziyaret bilgisi, veri tabanına kayıt edilmesi için kuyruğa eklendi.");
        }
    }
}
