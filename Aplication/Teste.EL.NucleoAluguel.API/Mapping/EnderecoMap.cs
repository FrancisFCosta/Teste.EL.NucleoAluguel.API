using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class EnderecoMap : Profile
    {
        public EnderecoMap()
        {
            CreateMap<Endereco, EnderecoModel>()
                .ForMember(dest => dest.IdEndereco, m => m.MapFrom(src => src.IdEndereco))
                .ForMember(dest => dest.CEP, m => m.MapFrom(src => src.CEP))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Estado));

            CreateMap<EnderecoModel, Endereco>()
                .ForMember(dest => dest.IdEndereco, m => m.MapFrom(src => src.IdEndereco))
                .ForMember(dest => dest.CEP, m => m.MapFrom(src => src.CEP))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Estado));
        }
    }
}
