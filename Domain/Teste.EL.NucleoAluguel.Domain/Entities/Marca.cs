using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Marca : BaseEntity
    {
        public int IdMarca { get; set; }
        public string DescricaoMarca { get; set; }
        public Marca(int idMarca, string descricao)
        {
            IdMarca = idMarca;
            DescricaoMarca = descricao;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(1, IdMarca, nameof(IdMarca), "O campo IdMarca é inválido"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(DescricaoMarca, nameof(DescricaoMarca), "O campo Descricao é obrigatório"));
        }
    }
}
