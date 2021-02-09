using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;

namespace Teste.EL.NucleoAluguel.Domain.Repositories
{
    public interface IVeiculoRepository
    {
        Veiculo Obter(int id);
        Veiculo ObterPorPlaca(string placa);
        List<Veiculo> ListarDisponivel();
        List<Veiculo> ListarPorCategoria(CategoriaVeiculo categoria);
        List<Veiculo> ListarPorCombustivel(TipoCombustivel combustivel);
        void Inserir(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Deletar(string placa);
        void AtualizarDisponibilidade(int idVeiculo, bool estaAlugado);
    }
}
