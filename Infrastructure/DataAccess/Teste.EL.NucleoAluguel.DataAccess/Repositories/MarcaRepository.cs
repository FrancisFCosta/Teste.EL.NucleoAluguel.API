using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        List<Marca> _marcaMock;
        public MarcaRepository()
        {
            _marcaMock = new List<Marca>();
            _marcaMock.Add(new Marca(1, "FIAT"));
            _marcaMock.Add(new Marca(2, "TOYOTA"));
        }
        public void Atualizar(Marca marca)
        {
            if (_marcaMock != null)
            {
                _marcaMock.RemoveAll(marcaMock => marcaMock.IdMarca == marca.IdMarca);
                _marcaMock.Add(marca);
            }
        }

        public void Deletar(int id)
        {
            if (_marcaMock != null)
                _marcaMock.RemoveAll(marcaMock => marcaMock.IdMarca == id);
        }

        public void Inserir(Marca marca)
        {
            if (marca != null)
                _marcaMock.Add(marca);
        }

        public List<Marca> Listar()
        {
            return _marcaMock;
        }

        public Marca Obter(int id)
        {
            if (_marcaMock != null)
                return _marcaMock.Where(marcaMock => marcaMock.IdMarca == id).FirstOrDefault();
            else
                return null;
        }
    }
}
