using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        List<Endereco> _enderecoMock;
        public EnderecoRepository()
        {
            _enderecoMock = new List<Endereco>();
            _enderecoMock.Add(new Endereco(1, "3051070", "Rua Candido de Souza", "3000 A", null, "Belo Horizonte", "Minas Gerais"));
            _enderecoMock.Add(new Endereco(2, "3051070", "Avenida Candido de Souza", "3000 B", null, "Belo Horizonte", "Minas Gerais"));
        }

        public void Atualizar(Endereco endereco)
        {
            if (_enderecoMock != null)
            {
                _enderecoMock.RemoveAll(enderecoMock => enderecoMock.IdEndereco == endereco.IdEndereco);
                _enderecoMock.Add(endereco);
            }
        }

        public void Deletar(int idEndereco)
        {
            if (_enderecoMock != null)
                _enderecoMock.RemoveAll(enderecoMock => enderecoMock.IdEndereco == idEndereco);
        }

        public int Inserir(Endereco endereco)
        {
            if (endereco != null)
            {
                int proximoId = _enderecoMock.Max(endereco => endereco.IdEndereco) + 1;

                endereco.IdEndereco = proximoId;
                _enderecoMock.Add(endereco);

                return proximoId;
            }

            return 0;
        }

        public List<Endereco> Listar()
        {
            return _enderecoMock;
        }

        public Endereco Obter(int idEndereco)
        {
            if (_enderecoMock != null)
                return _enderecoMock.Where(enderecoMock => enderecoMock.IdEndereco == idEndereco).FirstOrDefault();
            else
                return null;
        }
    }
}