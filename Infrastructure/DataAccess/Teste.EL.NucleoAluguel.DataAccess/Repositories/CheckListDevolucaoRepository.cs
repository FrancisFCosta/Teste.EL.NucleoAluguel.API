using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoCheckListDevolucao.Domain.Repositories;

namespace Teste.EL.NucleoCheckListDevolucao.DataAccess.Repositories
{
    public class CheckListDevolucaoRepository : ICheckListDevolucaoRepository
    {
        List<CheckListDevolucao> _checkListDevolucaoMock;
        public CheckListDevolucaoRepository()
        {
            _checkListDevolucaoMock = new List<CheckListDevolucao>();
        }

        public void Deletar(int idCheckListDevolucao)
        {
            if (_checkListDevolucaoMock != null)
                _checkListDevolucaoMock.RemoveAll(modeloMock => modeloMock.IdCheckListDevolucao == idCheckListDevolucao);
        }

        public void Inserir(CheckListDevolucao checkListDevolucao)
        {
            if (checkListDevolucao != null)
                _checkListDevolucaoMock.Add(checkListDevolucao);
        }
        public void Atualizar(CheckListDevolucao checkListDevolucao)
        {
            if (checkListDevolucao != null)
            {
                _checkListDevolucaoMock.RemoveAll(item => item.IdCheckListDevolucao == checkListDevolucao.IdCheckListDevolucao);
                _checkListDevolucaoMock.Add(checkListDevolucao);
            }
        }

        public CheckListDevolucao ObterPorAluguel(int idAluguel)
        {
            if (_checkListDevolucaoMock != null)
                return _checkListDevolucaoMock.Where(checkListDevolucaoMock => checkListDevolucaoMock.IdAluguel == idAluguel).FirstOrDefault();
            else
                return null;
        }

        public CheckListDevolucao Obter(int idCheckListDevolucao)
        {
            if (_checkListDevolucaoMock != null)
                return _checkListDevolucaoMock.Where(checkListDevolucaoMock => checkListDevolucaoMock.IdCheckListDevolucao == idCheckListDevolucao).FirstOrDefault();
            else
                return null;
        }
    }
}
