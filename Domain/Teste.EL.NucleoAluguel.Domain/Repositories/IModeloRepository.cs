using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IModeloRepository
    {
        List<Modelo> Listar();
        Modelo Obter(int idModelo);
        void Inserir(Modelo modelo);
        void Atualizar(Modelo modelo);
        void Deletar(int idModelo);
    }
}
