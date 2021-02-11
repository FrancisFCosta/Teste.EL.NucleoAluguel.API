using System;
using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        List<Aluguel> _aluguelMock;
        public AluguelRepository()
        {
            _aluguelMock = new List<Aluguel>();
            _aluguelMock.Add(new Aluguel(1, 1, 1, CategoriaVeiculo.Completo, DateTime.Now.AddDays(1), 26.3, 8, 0));
        }

        public void Deletar(int idAluguel)
        {
            if (_aluguelMock != null)
                _aluguelMock.RemoveAll(modeloMock => modeloMock.IdAluguel == idAluguel);
        }

        public int Inserir(Aluguel aluguel)
        {
            int proximoId = 0;

            if (aluguel != null)
            {
                proximoId = _aluguelMock.Max(al => al.IdAluguel);
                proximoId++;

                aluguel.IdAluguel = proximoId;
                _aluguelMock.Add(aluguel);
            }
            return proximoId;
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
