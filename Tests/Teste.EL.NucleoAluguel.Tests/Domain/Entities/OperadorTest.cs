using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class OperadorTest
    {
        [Fact]
        public void CriarOperador_OperadorInvalido_Test()
        {
            var Operador = new Operador(null, null);

            Assert.True(Operador.Invalid);
            Assert.Contains(Operador.Notifications, n => n.Property == nameof(Operador.Matricula));
            Assert.Contains(Operador.Notifications, n => n.Property == nameof(Operador.Nome));
        }

        [Fact]
        public void CriarOperador_OperadorValido_Test()
        {
            var Operador = new Operador("137108", "Francis Costa Operador");

            Assert.True(Operador.Valid);
        }
    }
}
