using Accommodations.App.Accommodations.Commands.CreateAccommodation;
using Accommodations.App.Accommodations.Commands.UpdateAccommodation;
using Accommodations.Domain.Entities;
using AutoMapper;

namespace Accommodations.App.Accommodations.Dtos
{
    public class AccommodationsProfile : Profile
    {
        public AccommodationsProfile()
        {
            CreateMap<UpdateAccommodationCommand, Accommodation>()
                .ForMember(d => d.Type, opt => opt.MapFrom((src, dest) => src.Type != null ? Enum.Parse<AccommodationType>(src.Type, true) : dest.Type))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));   

             CreateMap<CreateAccommodationCommand, Accommodation>()
                .ForMember(d => d.Type, opt => opt.MapFrom(src => Enum.Parse<AccommodationType>(src.Type, true)))
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                }));

            CreateMap<Accommodation, AccommodationDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(d => d.Units, opt => opt.MapFrom(src => src.Units));
        }
    }
}
