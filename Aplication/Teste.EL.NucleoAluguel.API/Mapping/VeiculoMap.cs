using AutoMapper;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoVeiculo.API.Mapping
{
    public class VeiculoMap : Profile
    {
        public VeiculoMap()
        {
            CreateMap<Veiculo, VeiculoModel>()
                .ForMember(dest => dest.Placa, m => m.MapFrom(src => src.Placa))
                .ForPath(dest => dest.Marca.IdMarca, m => m.MapFrom(src => src.IdMarca))
                .ForPath(dest => dest.Modelo.IdModelo, m => m.MapFrom(src => src.IdModelo))
                .ForMember(dest => dest.ValorHora, m => m.MapFrom(src => src.ValorHora))
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => Enums.GetName(src.Categoria)))
                .ForMember(dest => dest.LimitePortamalas, m => m.MapFrom(src => src.LimitePortamalas))
                .ForMember(dest => dest.Combustivel, m => m.MapFrom(src => Enums.GetName(src.Combustivel)));

            CreateMap<VeiculoModel, Veiculo>()
                .ForMember(dest => dest.Placa, m => m.MapFrom(src => src.Placa))
                .ForMember(dest => dest.IdMarca, m => m.MapFrom(src => src.Marca.IdMarca))
                .ForMember(dest => dest.IdModelo, m => m.MapFrom(src => src.Modelo.IdModelo))
                .ForMember(dest => dest.ValorHora, m => m.MapFrom(src => src.ValorHora))
                .ForMember(dest => dest.Categoria, m => m.MapFrom(src => Enums.Parse<CategoriaVeiculo>(src.Categoria)))
                .ForMember(dest => dest.LimitePortamalas, m => m.MapFrom(src => src.LimitePortamalas))
                .ForMember(dest => dest.Combustivel, m => m.MapFrom(src => Enums.Parse<CategoriaVeiculo>(src.Combustivel)));
        }
    }
}