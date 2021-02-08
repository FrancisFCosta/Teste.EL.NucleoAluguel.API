using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        List<Aluguel> _aluguelMock;
        public AluguelRepository()
        {
            _aluguelMock = new List<Aluguel>();
        }

        public void Deletar(int idAluguel)
        {
            if (_aluguelMock != null)
                _aluguelMock.RemoveAll(modeloMock => modeloMock.IdAluguel == idAluguel);
        }

        public void Inserir(Aluguel aluguel)
        {
            if (aluguel != null)
                _aluguelMock.Add(aluguel);
        }
        public void Atualizar(Aluguel aluguel)
        {
            if (aluguel != null)
            {
                _aluguelMock.RemoveAll(item => item.IdAluguel == aluguel.IdAluguel);
                _aluguelMock.Add(aluguel);
            }
        }

        public List<Aluguel> ListarPorCliente(int idCliente)
        {
            if (_aluguelMock != null)
                return _aluguelMock.Where(aluguelMock => aluguelMock.IdCliente == idCliente).ToList();
            else
                return null;
        }

        public Aluguel Obter(int idAluguel)
        {
            if (_aluguelMock != null)
                return _aluguelMock.Where(aluguelMock => aluguelMock.IdAluguel == idAluguel).FirstOrDefault();
            else
                return null;
        }
    }
}
