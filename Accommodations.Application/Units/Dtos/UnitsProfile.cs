using Accommodations.App.Units.Commands.CreateUnit;
using Accommodations.Domain.Entities;
using AutoMapper;


namespace Accommodations.App.Units.Dtos
{
    public class UnitsProfile : Profile
    {
        public UnitsProfile() 
        {
            CreateMap<CreateUnitCommand, Unit>()
                .ForMember(d => d.Type, opt => opt.MapFrom(src => Enum.Parse<UnitType>(src.Type, true)))
                .ForMember(d => d.BillingPeriod, opt => opt.MapFrom(src => Enum.Parse<BillingPeriod>(src.BillingPeriod, true)))
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                }));

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
