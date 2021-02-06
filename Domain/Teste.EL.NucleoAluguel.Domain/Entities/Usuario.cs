using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
