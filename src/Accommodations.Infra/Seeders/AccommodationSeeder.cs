
using Accommodations.Domain.Constants;
using Accommodations.Domain.Entities;
using Accommodations.Infra.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Accommodations.Infra.Seeders
{
    internal class AccommodationSeeder(AccommodationsDbContext _dbContext) : IAccommodationSeeder
    {
        public async Task Seed()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }

            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Accommodations.Any())
                {
                    var accommodations = GetAccommodations();
                    _dbContext.Accommodations.AddRange(accommodations);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.User)
                    {
                        NormalizedName = UserRoles.User.ToUpper(),
                    },
                    new (UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.Owner.ToUpper(),
                    },
                    new (UserRoles.Admin)
                    {
                        NormalizedName = UserRoles.Admin.ToUpper(),
                    },
                ];

            return roles;
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
                            },
                            IsAvailable = true,
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
                            },
                            IsAvailable = true,
                        }
                    }
                }
            };
        }


    }
}
