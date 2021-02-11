using Flunt.Validations;
using System;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Aluguel : BaseEntity
    {
        public int IdAluguel { get; set; }
        public int IdVeiculo { get; set; }
        public int IdCliente { get; set; }
        public CategoriaVeiculo Categoria { get; set; }
        public DateTime DataPrevistaAluguel { get; set; }
        public double ValorHora { get; set; }
        public double TotalDeHoras { get; set; }
        public double ValorFinal { get; set; }

        public Aluguel() { }
        public Aluguel(int idAluguel, int idVeiculo, int idCliente, CategoriaVeiculo categoria, DateTime dataPrevistaAluguel, double valorHora, double totalDeHoras, double valorFinal)
        {
            IdAluguel = idAluguel;
            IdVeiculo = idVeiculo;
            IdCliente = idCliente;
            Categoria = categoria;
            ValorHora = valorHora;
            DataPrevistaAluguel = dataPrevistaAluguel;
            TotalDeHoras = totalDeHoras;
            ValorFinal = valorFinal;

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

            AddNotifications(new Contract()
                .Requires()
                .IsLowerThan(DateTime.Today, DataPrevistaAluguel, nameof(DataPrevistaAluguel), "O campo TotalDeHoras é inválido"));
        }
    }
}
