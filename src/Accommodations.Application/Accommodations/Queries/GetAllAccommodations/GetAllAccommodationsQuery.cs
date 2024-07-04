using Accommodations.App.Accommodations.Dtos;
using Accommodations.App.Common;
using Accommodations.Domain.Constants;
using MediatR;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations
{
    public class GetAllAccommodationsQuery : IRequest<PagedResult<AccommodationDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; }
        public SortDirection sortDirection {  get; set; }
    }
}
