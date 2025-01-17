﻿using Accommodations.App.Accommodations.Dtos;
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

            var (accommodations, totalCount) = await accommodationsRepository.GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize, request.PageNumber, request.SortBy, request.sortDirection);
            var accommodationsdto = mapper.Map<IEnumerable<AccommodationDto>>(accommodations);

            var result = new PagedResult<AccommodationDto>(accommodationsdto, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
