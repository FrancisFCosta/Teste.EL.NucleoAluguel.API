using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class VeiculoTest
    {
        [Fact]
        public void CriarVeiculo_VeiculoInvalido_Test()
        {
            var Veiculo = new Veiculo(null, 0, 0, null, -1, TipoCombustivel.Alcool, -1, CategoriaVeiculo.Completo);

            Assert.True(Veiculo.Invalid);
            Assert.Contains(Veiculo.Notifications, n => n.Property == nameof(Veiculo.Placa));
            Assert.Contains(Veiculo.Notifications, n => n.Property == nameof(Veiculo.IdMarca));
            Assert.Contains(Veiculo.Notifications, n => n.Property == nameof(Veiculo.IdModelo));
            Assert.Contains(Veiculo.Notifications, n => n.Property == nameof(Veiculo.Ano));
            Assert.Contains(Veiculo.Notifications, n => n.Property == nameof(Veiculo.ValorHora));
        }

        [Fact]
        public void CriarVeiculo_VeiculoValido_Test()
        {
            var Veiculo = new Veiculo("QXH6632", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo);

            Assert.True(Veiculo.Valid);
        }
    }
}
