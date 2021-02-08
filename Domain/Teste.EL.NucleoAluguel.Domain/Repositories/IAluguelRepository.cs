using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IAluguelRepository
    {
        Aluguel Obter(int idAluguel);
        List<Aluguel> ListarPorCliente(int idCliente);
        void Inserir(Aluguel aluguel);
        void Atualizar(Aluguel aluguel);
        void Deletar(int idAluguel);
    }
}
