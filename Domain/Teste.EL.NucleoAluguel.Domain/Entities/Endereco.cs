using Flunt.Validations;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Endereco : BaseEntity
    {
        public int IdEndereco { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Endereco() { }
        public Endereco(int idEndereco, string cep, string logradouro, string numero, string complemento, string cidade, string estado)
        {
            IdEndereco = idEndereco;
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(CEP, nameof(CEP), "O campo CEP é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Logradouro, nameof(Logradouro), "O campo Logradouro é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(Numero), "O campo Numero é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Cidade, nameof(Cidade), "O campo Cidade é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Estado, nameof(Estado), "O campo Estado é obrigatório"));
        }
    }
}
