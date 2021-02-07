using System.ComponentModel;

namespace Teste.EL.NucleoAluguel.Domain.Enums
{
    public enum TipoCombustivel
    {
        [Description("Álcool")]
        Alcool = 1,
        [Description("Gasolina")]
        Gasolina = 2,
        [Description("Diesel")]
        Diesel = 3
    }
}
