using AutoMapper;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class MarcaMap : Profile
    {
        public MarcaMap()
        {
            CreateMap<Marca, MarcaModel>()
                .ForMember(dest => dest.IdMarca, m => m.MapFrom(src => src.IdMarca))
                .ForMember(dest => dest.DescricaoMarca, m => m.MapFrom(src => src.DescricaoMarca));

            CreateMap<MarcaModel, Marca>()
                .ForMember(dest => dest.IdMarca, m => m.MapFrom(src => src.IdMarca))
                .ForMember(dest => dest.DescricaoMarca, m => m.MapFrom(src => src.DescricaoMarca));
        }
    }
}
