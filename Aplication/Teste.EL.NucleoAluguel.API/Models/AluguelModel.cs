using System;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.API.Models
{
    public class AluguelModel
    {
        public int IdAluguel { get; set; }
        public VeiculoModel Veiculo { get; set; }
        public ClienteModel Cliente { get; set; }
        public CategoriaVeiculo Categoria { get; set; }
        public DateTime DataPrevistaAluguel { get; set; }
        public double ValorHora { get; set; }
        public double ValorFinal { get; set; }
        public double TotalDeHoras { get; set; }

        public AluguelModel() { }
    }
}
