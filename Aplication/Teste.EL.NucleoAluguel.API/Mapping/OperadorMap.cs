using AutoMapper;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class OperadorMap : Profile
    {
        public OperadorMap()
        {
            CreateMap<Operador, OperadorModel>()
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome));

            CreateMap<OperadorModel, Operador>()
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome));
        }
    }
}