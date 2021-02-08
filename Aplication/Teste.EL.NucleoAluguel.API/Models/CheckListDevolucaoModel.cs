using System;

namespace Teste.EL.NucleoAluguel.API.Models
{
    public class CheckListDevolucaoModel
    {
        public int IdCheckListDevolucao { get; set; }
        public AluguelModel Aluguel { get; set; }
        public VeiculoModel Veiculo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool CarroLimpo { get; set; }
        public bool PossuiArranhoes { get; set; }
        public bool PossuiAmassados { get; set; }
        public bool PossuiTanqueCheio { get; set; }
        public double ValorAluguel { get; set; }
        public double TotalCobrancaAdicional { get; set; }

        public CheckListDevolucaoModel() 
        {
            Aluguel = new AluguelModel();
            Veiculo = new VeiculoModel();
        }
    }
}
