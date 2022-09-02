using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        List<Cliente> _clienteMock;
        public ClienteRepository()
        {
            _clienteMock = new List<Cliente>();
            _clienteMock.Add(new Cliente(1, "Francis Costa", "12345678910", new DateTime(1996, 4, 5), 1, "francisf.costa@hotmail.com", "31991727960"));
        }

        public void Atualizar(Cliente cliente)
        {
            if (_clienteMock != null)
            {
                _clienteMock.RemoveAll(_usuariosMock => _usuariosMock.IdCliente == cliente.IdCliente);
                _clienteMock.Add(cliente);
            }
        }

        public void Deletar(int id)
        {
            if (_clienteMock != null)
                _clienteMock.RemoveAll(clienteMock => clienteMock.IdCliente == id);
        }

        public int Inserir(Cliente cliente)
        {
            int proximoId = 0;

            if (cliente != null)
            {
                proximoId = _clienteMock.Max(al => al.IdCliente);
                proximoId++;

                cliente.IdCliente = proximoId;
                _clienteMock.Add(cliente);
            }
            return proximoId;
        }

        public Cliente Obter(int id)
        {
            if (_clienteMock != null)
                return _clienteMock.Where(clienteMock => clienteMock.IdCliente == id).FirstOrDefault();
            else
                return null;
        }
        public Cliente ObterPorCPF(string cpf)
        {
            if (_clienteMock != null)
                return _clienteMock.Where(clienteMock => clienteMock.CPF == cpf).FirstOrDefault();
            else
                return null;
        }
    }
}
