﻿using Accommodations.Domain.Entities;

namespace Accommodations.Domain.Repositories
{
    public interface IAccommodationsRepository
    {
        Task<IEnumerable<Accommodation>> GetAllAsync();
    }
}
