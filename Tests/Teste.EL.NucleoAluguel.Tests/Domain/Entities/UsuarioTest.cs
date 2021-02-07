using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Xunit;

namespace Teste.EL.NucleoAluguel.Tests.Domain.Entities
{
    public class UsuarioTest
    {
        [Fact]
        public void CriarUsuario_UsuarioInvalido_Test()
        {
            var usuario = new Usuario(0,PerfilUsuario.Operador,null,null);

            Assert.True(usuario.Invalid);
            Assert.Contains(usuario.Notifications, n => n.Property == nameof(Usuario.IdUsuario));
            Assert.Contains(usuario.Notifications, n => n.Property == nameof(Usuario.Login));
            Assert.Contains(usuario.Notifications, n => n.Property == nameof(Usuario.Senha));
        }

        [Fact]
        public void CriarUsuario_UsuarioValido_Test()
        {
            var Usuario = new Usuario(1, PerfilUsuario.Cliente,"12542471657","loca@2020");

            Assert.True(Usuario.Valid);
        }

        [Fact]
        public void CriarUsuario_ClienteCPFValido_Test()
        {
            var usuario = new Usuario(1, PerfilUsuario.Cliente, "MG125424716", "loca@2020");

            Assert.True(usuario.Invalid);
            Assert.Contains(usuario.Notifications, n => n.Property == nameof(Usuario.Login));
        }
    }
}
