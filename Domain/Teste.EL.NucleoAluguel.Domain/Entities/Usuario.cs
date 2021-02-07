using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public int IdUsuario { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario(int idUsuario, PerfilUsuario perfil, string login, string senha) 
        {
            IdUsuario = idUsuario;
            Perfil = perfil;
            Login = login;
            Senha = senha;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Login, nameof(Login), "O campo Login é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Senha, nameof(Senha), "O campo Senha é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, IdUsuario, nameof(IdUsuario), "O campo IdUsuario é inválido"));

            if (Perfil == PerfilUsuario.Cliente)
            {
                AddNotifications(new Contract()
                .Requires()
                .HasLen(Login, 11, nameof(Login), "O campo é Login inválido: O campo CPF deve conter 11 dígitos")
                .IsDigit(Login, nameof(Login), "O campo é Login inválido: O campo CPF deve conter apenas números"));
            }
        }
    }
}
