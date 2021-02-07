using Flunt.Validations;
using System;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Aniversario { get; set; }
        public int IdEndereco { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }

        public Cliente(int idCliente, string nome, string cpf, DateTime aniversario, int idEndereco, string email, string celular)
        {
            IdCliente = idCliente;
            Nome = nome;
            CPF = cpf;
            Aniversario = aniversario;
            IdEndereco = idEndereco;
            Email = email;
            Celular = celular;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdCliente, nameof(IdCliente), "O campo IdCliente é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), "O campo Nome é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(CPF, nameof(CPF), "O campo CPF é obrigatório")
                .HasExactLengthIfNotNullOrEmpty(CPF, 11, nameof(CPF), "O campo CPF deve conter 11 dígitos")
                .IsDigit(CPF, nameof(CPF), "O campo CPF deve conter apenas números"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdEndereco, nameof(IdEndereco), "O campo IdEndereco é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Email, nameof(Email), "O campo Email não está em um formato correto"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Celular, nameof(Celular), "O campo Celular é obrigatório"));
        }
    }
}
