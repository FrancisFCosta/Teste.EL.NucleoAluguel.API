using Flunt.Validations;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Aluguel : BaseEntity
    {
        public int IdAluguel { get; set; }
        public int IdVeiculo { get; set; }
        public int IdCliente { get; set; }
        public CategoriaVeiculo Categoria { get; set; }
        public double ValorHora { get; set; }
        public double TotalDeHoras { get; set; }

        public Aluguel(int idAluguel, int idVeiculo, int idCliente, CategoriaVeiculo categoria, double valorHora, double totalDeHoras)
        {
            IdAluguel = idAluguel;
            IdVeiculo = idVeiculo;
            IdCliente = idCliente;
            Categoria = categoria;
            ValorHora = valorHora;
            TotalDeHoras = totalDeHoras;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdAluguel, nameof(IdAluguel), "O campo IdAluguel é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdVeiculo, nameof(IdVeiculo), "O campo IdVeiculo é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdCliente, nameof(IdCliente), "O campo IdCliente é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(0, ValorHora, nameof(ValorHora), "O campo ValorHora é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(1, TotalDeHoras, nameof(TotalDeHoras), "O campo TotalDeHoras é inválido"));
        }
    }
}
