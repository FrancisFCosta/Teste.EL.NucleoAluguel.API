using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
        List<Modelo> _modeloMock;
        public ModeloRepository()
        {
            _modeloMock = new List<Modelo>();
            _modeloMock.Add(new Modelo(1, "ARGO DRIVE 1.0"));
            _modeloMock.Add(new Modelo(2, "KAH SEDAN 1.5"));
        }
        public void Atualizar(Modelo modelo)
        {
            if (_modeloMock != null)
            {
                _modeloMock.RemoveAll(modeloMock => modeloMock.IdModelo == modelo.IdModelo);
                _modeloMock.Add(modelo);
            }
        }

        public void Deletar(int id)
        {
            if (_modeloMock != null)
                _modeloMock.RemoveAll(modeloMock => modeloMock.IdModelo == id);
        }

        public void Inserir(Modelo modelo)
        {
            if (modelo != null)
                _modeloMock.Add(modelo);
        }

        public List<Modelo> Listar()
        {
            return _modeloMock;
        }

        public Modelo Obter(int id)
        {
            if (_modeloMock != null)
                return _modeloMock.Where(modeloMock => modeloMock.IdModelo == id).FirstOrDefault();
            else
                return null;
        }
    }
}
