using System.ComponentModel;

namespace Teste.EL.NucleoAluguel.Domain.Enums
{
    public enum CategoriaVeiculo
    {
        [Description("Básico")]
        Basico = 1,
        [Description("Completo")]
        Completo = 2,
        [Description("Luxo")]
        Luxo = 3
    }
}
