using AutoMapper;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.API.Mapping
{
    public class UsuarioMap : Profile
    {
        public UsuarioMap()
        {
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(dest => dest.IdUsuario, m => m.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.Login, m => m.MapFrom(src => src.Login))
                .ForMember(dest => dest.Perfil, m => m.MapFrom(src => src.Perfil));

            CreateMap<UsuarioModel, Usuario>()
                .ForMember(dest => dest.IdUsuario, m => m.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.Login, m => m.MapFrom(src => src.Login))
                .ForMember(dest => dest.Perfil, m => m.MapFrom(src => src.Perfil))
                .ForMember(dest => dest.Senha, m => m.MapFrom(src => src.Senha));
        }
    }
}