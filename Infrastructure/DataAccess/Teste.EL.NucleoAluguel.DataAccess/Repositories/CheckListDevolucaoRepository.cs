using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
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

        public int Inserir(CheckListDevolucao checkListDevolucao)
        {
            int proximoId = 0;

            if (checkListDevolucao != null)
            {
                proximoId = _checkListDevolucaoMock.Max(al => al.IdAluguel);
                proximoId++;

                checkListDevolucao.IdAluguel = proximoId;
                _checkListDevolucaoMock.Add(checkListDevolucao);
            }
            return proximoId;
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
