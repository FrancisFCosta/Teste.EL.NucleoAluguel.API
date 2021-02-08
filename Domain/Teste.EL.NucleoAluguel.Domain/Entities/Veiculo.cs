using Flunt.Validations;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Veiculo : BaseEntity
    {
        public int IdVeiculo { get; set; }
        public string Placa { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public string Ano { get; set; }
        public double ValorHora { get; set; }
        public TipoCombustivel Combustivel { get; set; }
        public double LimitePortamalas { get; set; }
        public CategoriaVeiculo Categoria { get; set; }
        public Veiculo(int idVeiculo, string placa, int idMarca, int idModelo, string ano, double valorHora, TipoCombustivel tipoCombustivel, double limitePortamalas, CategoriaVeiculo categoria)
        {
            IdVeiculo = idVeiculo;
            Placa = placa;
            IdMarca = idMarca;
            IdModelo = idModelo;
            Ano = ano;
            ValorHora = valorHora;
            Combustivel = tipoCombustivel;
            LimitePortamalas = limitePortamalas;
            Categoria = categoria;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Placa, nameof(Placa), "O campo Placa é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdMarca, nameof(IdMarca), "O campo IdMarca é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdModelo, nameof(IdModelo), "O campo IdModelo é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Ano, nameof(Ano), "O campo Ano é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, ValorHora, nameof(ValorHora), "O campo ValorHora é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(0, LimitePortamalas, nameof(LimitePortamalas), "O campo LimitePortamalas é inválido"));
        }
    }
}
