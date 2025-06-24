using AutoMapper;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CheeseDTO, Cheese>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}