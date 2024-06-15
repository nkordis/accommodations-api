using Accommodations.Domain.Entities;
using AutoMapper;


namespace Accommodations.App.Units.Dtos
{
    public class UnitsProfile : Profile
    {
        public UnitsProfile() 
        {
            CreateMap<Unit, UnitDto>()
                .ForMember(d => d.City, opt => 
                    opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.Street, opt => 
                    opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.PostalCode, opt => 
                    opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode));
                
        }
    }
}
