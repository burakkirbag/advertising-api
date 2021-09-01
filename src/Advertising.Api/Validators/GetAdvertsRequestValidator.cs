using Advertising.Api.Models.Requests;
using FluentValidation;

namespace Advertising.Api.Validators
{
    public class GetAdvertsRequestValidator : AbstractValidator<GetAdvertsRequest>
    {
        public GetAdvertsRequestValidator()
        {
            When(x => x.MinPrice.HasValue, () =>
            {
                RuleFor(x => x.MinPrice).GreaterThanOrEqualTo(0).WithMessage("Minimum fiyat en az 0 olabilir.");
            });

            When(x => x.MaxPrice.HasValue, () =>
            {
                RuleFor(x => x.MaxPrice).GreaterThan(x => 0).WithMessage("Maksimum fiyat 0'dan büyük olmalıdır.");
            });

            When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue, () =>
            {
                RuleFor(x => x.MaxPrice).GreaterThan(x => x.MinPrice).WithMessage("Maksimum fiyat, minimum fiyattan küçük olamaz.");
            });

            When(x => !string.IsNullOrEmpty(x.SortType), () =>
            {
                RuleFor(x => x.SortType).Must(x => x.Equals("asc", System.StringComparison.InvariantCultureIgnoreCase) || x.Equals("desc", System.StringComparison.InvariantCultureIgnoreCase)).WithMessage("Geçersiz bir sıralama tipi girdiniz.");
            });
        }
    }
}
