using Accommodations.App.Accommodations.Dtos;
using Accommodations.App.Common;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations
{
    public class GetAllAccommodationsQueryHandler(ILogger<GetAllAccommodationsQueryHandler> logger,
        IMapper mapper, IAccommodationsRepository accommodationsRepository) 
            : IRequestHandler<GetAllAccommodationsQuery, PagedResult<AccommodationDto>>
    {
        public async Task<PagedResult<AccommodationDto>> Handle(GetAllAccommodationsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all accommodations");
            // Set default values if they are not provided
            var pageSize = request.PageSize > 0 ? request.PageSize : 10; 
            var pageNumber = request.PageNumber > 0 ? request.PageNumber : 1; 

            var (accommodations, totalCount) = await accommodationsRepository.GetAllMatchingAsync(request.SearchPhrase,
                pageSize, pageNumber);
            var accommodationsdto = mapper.Map<IEnumerable<AccommodationDto>>(accommodations);

            var result = new PagedResult<AccommodationDto>(accommodationsdto, totalCount, pageSize, pageNumber);
            return result;
        }
    }
}
