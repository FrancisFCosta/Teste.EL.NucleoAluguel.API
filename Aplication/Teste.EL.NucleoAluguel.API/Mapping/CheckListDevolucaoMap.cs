using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoCheckListDevolucao.API.Mapping
{
    public class CheckListDevolucaoMap : Profile
    {
        public CheckListDevolucaoMap()
        {
            CreateMap<CheckListDevolucao, CheckListDevolucaoModel>()
                .ForMember(dest => dest.IdCheckListDevolucao, m => m.MapFrom(src => src.IdCheckListDevolucao))
                .ForPath(dest => dest.Aluguel.IdAluguel, m => m.MapFrom(src => src.IdAluguel))
                .ForPath(dest => dest.Veiculo.IdVeiculo, m => m.MapFrom(src => src.IdVeiculo))
                .ForMember(dest => dest.CarroLimpo, m => m.MapFrom(src => src.CarroLimpo))
                .ForMember(dest => dest.PossuiAmassados, m => m.MapFrom(src => src.PossuiAmassados))
                .ForMember(dest => dest.PossuiArranhoes, m => m.MapFrom(src => src.PossuiArranhoes))
                .ForMember(dest => dest.PossuiTanqueCheio, m => m.MapFrom(src => src.PossuiTanqueCheio))
                .ForMember(dest => dest.ValorAluguel, m => m.MapFrom(src => src.ValorAluguel))
                .ForMember(dest => dest.TotalCobrancaAdicional, m => m.MapFrom(src => src.TotalCobrancaAdicional));

            CreateMap<CheckListDevolucaoModel, CheckListDevolucao>()
                .ForMember(dest => dest.IdCheckListDevolucao, m => m.MapFrom(src => src.IdCheckListDevolucao))
                .ForPath(dest => dest.IdAluguel, m => m.MapFrom(src => src.Aluguel.IdAluguel))
                .ForPath(dest => dest.IdVeiculo, m => m.MapFrom(src => src.Veiculo.IdVeiculo))
                .ForMember(dest => dest.CarroLimpo, m => m.MapFrom(src => src.CarroLimpo))
                .ForMember(dest => dest.PossuiAmassados, m => m.MapFrom(src => src.PossuiAmassados))
                .ForMember(dest => dest.PossuiArranhoes, m => m.MapFrom(src => src.PossuiArranhoes))
                .ForMember(dest => dest.PossuiTanqueCheio, m => m.MapFrom(src => src.PossuiTanqueCheio))
                .ForMember(dest => dest.ValorAluguel, m => m.MapFrom(src => src.ValorAluguel))
                .ForMember(dest => dest.TotalCobrancaAdicional, m => m.MapFrom(src => src.TotalCobrancaAdicional));
        }
    }
}
