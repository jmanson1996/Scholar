using AutoMapper;

namespace Schola.Material.Dto
{
    public class MaterialProfileMapper : Profile
    {
        public MaterialProfileMapper()
        {
            CreateMap<Materiales.Material, MaterialViewDto>().ReverseMap();
            CreateMap<Materiales.Material, MaterialFullDto>().ReverseMap();
        }
    }
}
