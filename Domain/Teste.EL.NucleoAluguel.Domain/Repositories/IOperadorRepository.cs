using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IOperadorRepository
    {
        Operador Obter(string matricula);
        void Inserir(Operador operador);
        void Atualizar(Operador operador);
        void Deletar(string matricula);
    }
}
