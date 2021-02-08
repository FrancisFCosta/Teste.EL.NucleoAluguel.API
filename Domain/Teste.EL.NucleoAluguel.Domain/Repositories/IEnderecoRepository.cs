using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IEnderecoRepository
    {
        Endereco Obter(int idEndereco);
        int Inserir(Endereco endereco);
        void Atualizar(Endereco endereco);
        void Deletar(int idCliente);
    }
}
