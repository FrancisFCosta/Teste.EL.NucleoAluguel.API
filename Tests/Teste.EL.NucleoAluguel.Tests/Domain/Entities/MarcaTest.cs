using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class MarcaTest
    {
        [Fact]
        public void CriarMarca_MarcaInvalida_Test()
        {
            var Marca = new Marca(0, null);

            Assert.True(Marca.Invalid);
            Assert.Contains(Marca.Notifications, n => n.Property == nameof(Marca.IdMarca));
            Assert.Contains(Marca.Notifications, n => n.Property == nameof(Marca.DescricaoMarca));
        }

        [Fact]
        public void CriarMarca_MarcaValida_Test()
        {
            var Marca = new Marca(1, "FIAT");

            Assert.True(Marca.Valid);
        }
    }
}
