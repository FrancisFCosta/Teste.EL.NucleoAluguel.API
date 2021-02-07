using Teste.EL.NucleoAluguel.Domain.Entities;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class ModeloTest
    {
        [Fact]
        public void CriarModelo_ModeloInvalido_Test()
        {
            var Modelo = new Modelo(0, null);

            Assert.True(Modelo.Invalid);
            Assert.Contains(Modelo.Notifications, n => n.Property == nameof(Modelo.IdModelo));
            Assert.Contains(Modelo.Notifications, n => n.Property == nameof(Modelo.DescricaoModelo));
        }

        [Fact]
        public void CriarModelo_ModeloValido_Test()
        {
            var Modelo = new Modelo(1, "ARGO DRIVE 1.0");

            Assert.True(Modelo.Valid);
        }
    }
}
