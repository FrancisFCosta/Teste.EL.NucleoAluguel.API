using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IClienteRepository
    {        
        Cliente Obter(int idCliente);
        Cliente ObterPorCPF(string cpf);
        void Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Deletar(int id);
    }
}
