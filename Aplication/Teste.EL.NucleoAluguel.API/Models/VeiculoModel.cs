using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.API.Models
{
    public class VeiculoModel
    {
        public int IdVeiculo { get; set; }
        public string Placa { get; set; }
        public MarcaModel Marca { get; set; }
        public ModeloModel Modelo { get; set; }
        public string Ano { get; set; }
        public double ValorHora { get; set; }
        public string Combustivel { get; set; }
        public double LimitePortamalas { get; set; }
        public string Categoria { get; set; }

        public VeiculoModel() 
        {
            Marca = new MarcaModel();
            Modelo = new ModeloModel();
        }
    }
}
