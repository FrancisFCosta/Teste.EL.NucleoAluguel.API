using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class ClienteTest
    {
        [Fact]
        public void CriarCliente_ClienteInvalido_Test()
        {
            var Cliente = new Cliente(0, null, "ab23121", DateTime.MinValue, 0, "francis.francis", null);

            Assert.True(Cliente.Invalid);
            Assert.Contains(Cliente.Notifications, n => n.Property == nameof(Cliente.IdCliente));
            Assert.Contains(Cliente.Notifications, n => n.Property == nameof(Cliente.Nome));
            Assert.Contains(Cliente.Notifications, n => n.Property == nameof(Cliente.CPF));
            Assert.Contains(Cliente.Notifications, n => n.Property == nameof(Cliente.IdEndereco));
            Assert.Contains(Cliente.Notifications, n => n.Property == nameof(Cliente.Email));
            Assert.Contains(Cliente.Notifications, n => n.Property == nameof(Cliente.Celular));
        }

        [Fact]
        public void CriarCliente_ClienteValido_Test()
        {
            var Cliente = new Cliente(1, "Francis Costa", "12542471657", new DateTime(1996, 4, 5), 1, "francis.costa@localiza.com", "31991727960");

            Assert.True(Cliente.Valid);
        }
    }
}
