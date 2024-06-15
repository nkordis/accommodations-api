using Accommodations.Domain.Entities;
using AutoMapper;

namespace Accommodations.App.Accommodations.Dtos
{
    public class AccommodationsProfile : Profile
    {
        public AccommodationsProfile()
        {
            CreateMap<Accommodation, AccommodationDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(d => d.Units, opt => opt.MapFrom(src => src.Units));
        }
    }
}
