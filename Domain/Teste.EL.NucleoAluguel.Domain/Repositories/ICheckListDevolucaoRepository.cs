using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
     public interface ICheckListDevolucaoRepository
    {
        CheckListDevolucao Obter(int idCheckListDevolucao);
        CheckListDevolucao ObterPorAluguel(int idAluguel);
        int Inserir(CheckListDevolucao checkListDevolucao);
        void Atualizar(CheckListDevolucao checkListDevolucao);
        void Deletar(int idCheckListDevolucao);
    }
}