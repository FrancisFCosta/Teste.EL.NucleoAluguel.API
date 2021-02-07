using System.ComponentModel;

namespace Teste.EL.NucleoAluguel.Domain.Enums
{
    public enum PerfilUsuario
    {
        [Description("Cliente")]
        Cliente = 1,
        [Description("Operador")]
        Operador = 2
    }
}
