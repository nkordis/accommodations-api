﻿
using Accommodations.Domain.Entities;
using Accommodations.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Accommodations.Infra.Seeders
{
    internal class AccommodationSeeder(AccommodationsDbContext _dbContext) : IAccommodationSeeder
    {
        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Accommodations.Any())
                {
                    var accommodations = GetAccommodations();
                    _dbContext.Accommodations.AddRange(accommodations);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Accommodation> GetAccommodations()
        {
            return new List<Accommodation>
            {
                new Accommodation
                {
                    Id = Guid.NewGuid(),
                    Name = "Sunny Apartments",
                    Description = "Modern apartments with stunning city views.",
                    Type = AccommodationType.ApartmentComplex,
                    HasInstantBooking = true,
                    ContactEmail = "contact@sunnyapartments.com",
                    ContactNumber = "123-456-7890",
                    Address = new Address
                    {
                        City = "Copenhagen",
                        Street = "123 Main Street",
                        PostalCode = "1000"
                    },
                    Units = new List<Unit>
                    {
                        new Unit
                        {
                            Id = Guid.NewGuid(),
                            Name = "Studio Apartment",
                            Description = "Cozy studio apartment with all amenities.",
                            Price = 100m,
                            BillingPeriod = BillingPeriod.PerDay,
                            Capacity = 2,
                            Type = UnitType.Apartment,
                            Address = new Address
                            {
                                City = "Copenhagen",
                                Street = "123 Main Street",
                                PostalCode = "1000"
                            }
                        }
                    }
                },
                new Accommodation
                {
                    Id = Guid.NewGuid(),
                    Name = "Cozy Cottage",
                    Description = "Charming cottage in a serene location.",
                    Type = AccommodationType.GuestHouse,
                    HasInstantBooking = false,
                    ContactEmail = "info@cozycottage.com",
                    ContactNumber = "987-654-3210",
                    Address = new Address
                    {
                        City = "Aarhus",
                        Street = "456 Country Road",
                        PostalCode = "8000"
                    },
                    Units = new List<Unit>
                    {
                        new Unit
                        {
                            Id = Guid.NewGuid(),
                            Name = "Private Room",
                            Description = "Comfortable private room with garden view.",
                            Price = 50m,
                            BillingPeriod = BillingPeriod.PerDay,
                            Capacity = 1,
                            Type = UnitType.Room,
                            Address = new Address
                            {
                                City = "Aarhus",
                                Street = "456 Country Road",
                                PostalCode = "8000"
                            }
                        }
                    }
                }
            };
        }


    }
}