using FluentValidation;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations
{
    public class GetAllAccommodationsQueryValidator : AbstractValidator<GetAllAccommodationsQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];

        public GetAllAccommodationsQueryValidator()
        {
            RuleFor(a => a.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(a => a.PageSize)
                .Must(value => allowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");
        }
    }
}
