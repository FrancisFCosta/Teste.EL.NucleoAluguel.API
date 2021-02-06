using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

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
                .IsLowerOrEqualsThan(1, IdUsuario, nameof(IdUsuario), "O campo IdUsuario é inválido"));
        }
    }
}
