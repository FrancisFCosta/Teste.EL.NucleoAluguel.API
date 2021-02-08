using AutoMapper;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class ModeloMap : Profile
    {
        public ModeloMap()
        {
            CreateMap<Modelo, ModeloModel>()
                .ForMember(dest => dest.IdModelo, m => m.MapFrom(src => src.IdModelo))
                .ForMember(dest => dest.DescricaoModelo, m => m.MapFrom(src => src.DescricaoModelo));

            CreateMap<ModeloModel, Modelo>()
                .ForMember(dest => dest.IdModelo, m => m.MapFrom(src => src.IdModelo))
                .ForMember(dest => dest.DescricaoModelo, m => m.MapFrom(src => src.DescricaoModelo));
        }
    }
}
