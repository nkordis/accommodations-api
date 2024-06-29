using Accommodations.App.Accommodations.Dtos;
using FluentValidation;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations
{
    public class GetAllAccommodationsQueryValidator : AbstractValidator<GetAllAccommodationsQuery>
    {
        private int[] allowPageSizes = [5, 10, 15, 30];
        private string[] allowedSortByColumnNames = [nameof(AccommodationDto.Name), 
            nameof(AccommodationDto.Description), nameof(AccommodationDto.Type), nameof(AccommodationDto.City)];

        public GetAllAccommodationsQueryValidator()
        {
            RuleFor(a => a.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(a => a.PageSize)
                .Must(value => allowPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

            RuleFor(a => a.SortBy)
                .Must(value => allowedSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
