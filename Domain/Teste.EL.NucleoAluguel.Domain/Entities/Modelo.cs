using Flunt.Validations;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Modelo : BaseEntity
    {
        public int IdModelo { get; set; }
        public string DescricaoModelo { get; set; }
        public Modelo(int idModelo, string descricao)
        {
            IdModelo = idModelo;
            DescricaoModelo = descricao;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdModelo, nameof(IdModelo), "O campo IdModelo é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(DescricaoModelo, nameof(DescricaoModelo), "O campo Descricao é obrigatório"));
        }
    }
}
