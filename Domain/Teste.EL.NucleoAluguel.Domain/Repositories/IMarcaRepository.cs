using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IMarcaRepository
    {
        List<Marca> Listar();
        Marca Obter(int idMarca);
        void Inserir(Marca marca);
        void Atualizar(Marca marca);
        void Deletar(int idMarca);
    }
}
