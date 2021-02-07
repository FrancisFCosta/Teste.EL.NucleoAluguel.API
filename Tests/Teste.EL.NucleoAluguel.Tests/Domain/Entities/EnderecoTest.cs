using Teste.EL.NucleoAluguel.Domain.Entities;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class EnderecoTest
    {
        [Fact]
        public void CriarEndereco_EnderecoInvalido_Test()
        {
            var Endereco = new Endereco(0, null, null, null, null, null, null);

            Assert.True(Endereco.Invalid);
            Assert.Contains(Endereco.Notifications, n => n.Property == nameof(Endereco.IdCliente));
            Assert.Contains(Endereco.Notifications, n => n.Property == nameof(Endereco.Logradouro));
            Assert.Contains(Endereco.Notifications, n => n.Property == nameof(Endereco.Numero));
            Assert.Contains(Endereco.Notifications, n => n.Property == nameof(Endereco.CEP));
            Assert.Contains(Endereco.Notifications, n => n.Property == nameof(Endereco.Cidade));
            Assert.Contains(Endereco.Notifications, n => n.Property == nameof(Endereco.Estado));
        }

        [Fact]
        public void CriarEndereco_EnderecoValido_Test()
        {
            var Endereco = new Endereco(1, "3051070", "Rua Candido de Souza", "3000 A", null, "Belo Horizonte", "Minas Gerais");

            Assert.True(Endereco.Valid);
        }
    }
}
