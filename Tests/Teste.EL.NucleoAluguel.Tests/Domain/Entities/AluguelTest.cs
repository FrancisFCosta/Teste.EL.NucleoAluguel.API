using System;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class AluguelTest
    {
        [Fact]
        public void CriarAluguel_AluguelInvalido_Test()
        {
            var Aluguel = new Aluguel(0, 0, 0, CategoriaVeiculo.Completo, DateTime.Now.AddDays(-1), 0, 0, 0);

            Assert.True(Aluguel.Invalid);
            Assert.Contains(Aluguel.Notifications, n => n.Property == nameof(Aluguel.IdAluguel));
            Assert.Contains(Aluguel.Notifications, n => n.Property == nameof(Aluguel.IdCliente));
            Assert.Contains(Aluguel.Notifications, n => n.Property == nameof(Aluguel.IdVeiculo));
            Assert.Contains(Aluguel.Notifications, n => n.Property == nameof(Aluguel.ValorHora));
            Assert.Contains(Aluguel.Notifications, n => n.Property == nameof(Aluguel.TotalDeHoras));
            Assert.Contains(Aluguel.Notifications, n => n.Property == nameof(Aluguel.DataPrevistaAluguel));
        }

        [Fact]
        public void CriarAluguel_AluguelValido_Test()
        {
            var Aluguel = new Aluguel(1, 1, 1, CategoriaVeiculo.Completo, DateTime.Now.AddDays(1), 70.6, 8, 0);

            Assert.True(Aluguel.Valid);
        }
    }
}
