using AutoMapper;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class AluguelMap : Profile
    {
        public AluguelMap()
        {
            CreateMap<Aluguel, AluguelModel>()
                .ForMember(dest => dest.IdAluguel, m => m.MapFrom(src => src.IdAluguel))
                .ForPath(dest => dest.Cliente.IdCliente, m => m.MapFrom(src => src.IdCliente))
                .ForPath(dest => dest.Veiculo.IdVeiculo, m => m.MapFrom(src => src.IdVeiculo))
                .ForMember(dest => dest.ValorHora, m => m.MapFrom(src => src.ValorHora))
                .ForMember(dest => dest.ValorFinal, m => m.MapFrom(src => src.ValorFinal))
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.DataPrevistaAluguel, m => m.MapFrom(src => src.DataPrevistaAluguel))
                .ForMember(dest => dest.TotalDeHoras, m => m.MapFrom(src => src.TotalDeHoras));

            CreateMap<AluguelModel, Aluguel>()
                .ForMember(dest => dest.IdAluguel, m => m.MapFrom(src => src.IdAluguel))
                .ForMember(dest => dest.IdCliente, m => m.MapFrom(src => src.Cliente.IdCliente))
                .ForMember(dest => dest.IdVeiculo, m => m.MapFrom(src => src.Veiculo.IdVeiculo))
                .ForMember(dest => dest.ValorHora, m => m.MapFrom(src => src.ValorHora))
                .ForMember(dest => dest.ValorFinal, m => m.MapFrom(src => src.ValorFinal))
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.DataPrevistaAluguel, m => m.MapFrom(src => src.DataPrevistaAluguel))
                .ForMember(dest => dest.TotalDeHoras, m => m.MapFrom(src => src.TotalDeHoras));
        }
    }
}