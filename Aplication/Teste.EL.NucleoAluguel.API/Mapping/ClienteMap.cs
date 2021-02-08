using AutoMapper;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<Cliente, ClienteModel>()
                .ForMember(dest => dest.IdCliente, m => m.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.CPF, m => m.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Aniversario, m => m.MapFrom(src => src.Aniversario))
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.Email))
                .ForMember(dest => dest.Celular, m => m.MapFrom(src => src.Celular));

            CreateMap<ClienteModel, Cliente>()
                .ForMember(dest => dest.IdCliente, m => m.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.CPF, m => m.MapFrom(src => src.CPF))
                .ForMember(dest => dest.Aniversario, m => m.MapFrom(src => src.Aniversario))
                .ForMember(dest => dest.IdEndereco, m => m.MapFrom(src => src.Endereco.IdEndereco))
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.Email))
                .ForMember(dest => dest.Celular, m => m.MapFrom(src => src.Celular));
        }
    }
}
