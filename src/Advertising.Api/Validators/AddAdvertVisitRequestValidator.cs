using Advertising.Api.Models.Requests;
using FluentValidation;

namespace Advertising.Api.Validators
{
    public class AddAdvertVisitRequestValidator : AbstractValidator<AddAdvertVisitRequest>
    {
        public AddAdvertVisitRequestValidator()
        {
            RuleFor(x => x.AdvertId).GreaterThan(0).WithMessage("Reklam kimlik numarasını belirtmelisiniz.");
        }
    }
}
