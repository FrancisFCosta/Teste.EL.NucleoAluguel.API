using Flunt.Validations;
using System;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class CheckListDevolucao : BaseEntity
    {
        public int IdAluguel { get; set; }
        public int IdVeiculo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool CarroLimpo { get; set; }
        public bool PossuiArranhoes { get; set; }
        public bool PossuiAmassados { get; set; }
        public bool PossuiTanqueCheio { get; set; }
        public double ValorAluguel { get; set; }
        public double TotalCobrancaAdicional { get => CalcularTotalCobrancaAdicional(); }

        public const double PercentualCobrancaAdicional = 30;

        public CheckListDevolucao(int idAluguel, int idVeiculo, DateTime dataDevolucao, bool carroLimpo, bool possuiArranhoes, bool possuiTanqueCheio, bool possuiAmassados, double valorAluguel)
        {
            IdAluguel = idAluguel;
            IdVeiculo = idVeiculo;
            DataDevolucao = dataDevolucao;
            CarroLimpo = carroLimpo;
            PossuiArranhoes = possuiArranhoes;
            PossuiAmassados = possuiAmassados;
            PossuiTanqueCheio = possuiTanqueCheio;
            ValorAluguel = valorAluguel;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdAluguel, nameof(IdAluguel), "O campo IdAluguel é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdVeiculo, nameof(IdVeiculo), "O campo IdVeiculo é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(0, ValorAluguel, nameof(ValorAluguel), "O campo ValorAluguel é inválido"));
        }

        private double CalcularTotalCobrancaAdicional() 
        {
            if (this.Invalid)
                return 0;

            double totalValorAdicional = 0;
            double taxaAdicional = ValorAluguel * (PercentualCobrancaAdicional / 100);

            if (!CarroLimpo)
                totalValorAdicional += taxaAdicional;
            if (PossuiArranhoes)
                totalValorAdicional += taxaAdicional;
            if (PossuiAmassados)
                totalValorAdicional += taxaAdicional;
            if (!PossuiTanqueCheio)
                totalValorAdicional += taxaAdicional;

            return totalValorAdicional;
        }
    }
}
