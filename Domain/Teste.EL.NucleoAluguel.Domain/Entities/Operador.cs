﻿using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.EL.NucleoAluguel.Domain.Entities
{
    public class Operador : BaseEntity
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public Operador(string matricula, string nome)
        {
            Matricula = matricula;
            Nome = nome;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Matricula, nameof(Matricula), "O campo Matrícula é obrigatório"));

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), "O campo Nome é obrigatório"));
        }
    }
}
