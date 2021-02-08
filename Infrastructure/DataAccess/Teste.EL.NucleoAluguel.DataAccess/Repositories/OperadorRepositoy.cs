using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class OperadorRepositoy : IOperadorRepository
    {
        List<Operador> _operadorMock;
        public OperadorRepositoy()
        {
            _operadorMock = new List<Operador>();
            _operadorMock.Add(new Operador("137108", "Francis Costa"));
            _operadorMock.Add(new Operador("191826", "Francis DTI"));
        }

        public void Atualizar(Operador operador)
        {
            if (_operadorMock != null)
            {
                _operadorMock.RemoveAll(_usuariosMock => _usuariosMock.Matricula == operador.Matricula);
                _operadorMock.Add(operador);
            }
        }

        public void Deletar(string matricula)
        {
            if (_operadorMock != null)
                _operadorMock.RemoveAll(operadorMock => operadorMock.Matricula == matricula);
        }

        public void Inserir(Operador operador)
        {
            if (operador != null)
                _operadorMock.Add(operador);
        }

        public Operador Obter(string matricula)
        {
            if (_operadorMock != null)
                return _operadorMock.Where(operadorMock => operadorMock.Matricula == matricula).FirstOrDefault();
            else
                return null;
        }
    }
}
