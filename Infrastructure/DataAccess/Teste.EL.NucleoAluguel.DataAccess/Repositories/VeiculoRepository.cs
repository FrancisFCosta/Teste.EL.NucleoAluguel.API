using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Enums;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.DataAccess.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        List<Veiculo> _veiculoMock;
        public VeiculoRepository()
        {
            _veiculoMock = new List<Veiculo>();
            _veiculoMock.Add(new Veiculo(1,"QXH6632", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo));
            _veiculoMock.Add(new Veiculo(2,"QUH1051", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo));
            _veiculoMock.Add(new Veiculo(3,"PQP7777", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo));
            _veiculoMock.Add(new Veiculo(4,"OMG7879", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo));
            _veiculoMock.Add(new Veiculo(5,"APO1074", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo));
            _veiculoMock.Add(new Veiculo(6,"ETA4542", 1, 1, "2020/2020", 150, TipoCombustivel.Alcool, 290, CategoriaVeiculo.Completo));
        }
        public void Atualizar(Veiculo veiculo)
        {
            if (_veiculoMock != null)
            {
                _veiculoMock.RemoveAll(veiculo => veiculo.IdVeiculo == veiculo.IdVeiculo);
                _veiculoMock.Add(veiculo);
            }
        }

        public void AtualizarDisponibilidade(int idVeiculo, bool estaAlugado)
        {
            if (_veiculoMock != null)
            {
                var copiaVeiculo = _veiculoMock.Where(veiculo => veiculo.IdVeiculo == idVeiculo).FirstOrDefault();
                copiaVeiculo.EstaAlugado = estaAlugado;
                _veiculoMock.RemoveAll(veiculo => veiculo.IdVeiculo == idVeiculo);
                _veiculoMock.Add(copiaVeiculo);
            }
        }

        public void Deletar(string placa)
        {
            if (_veiculoMock != null)
                _veiculoMock.RemoveAll(veiculoMock => veiculoMock.Placa.Trim().ToLower() == placa?.Trim()?.ToLower());
        }

        public void Inserir(Veiculo veiculo)
        {
            if (veiculo != null)
                _veiculoMock.Add(veiculo);
        }

        public Veiculo Obter(int idVeiculo)
        {
            if (_veiculoMock != null)
                return _veiculoMock.Where(veiculoMock => veiculoMock.IdVeiculo == idVeiculo).FirstOrDefault();
            else
                return null;
        }

        public Veiculo ObterPorPlaca(string placa)
        {
            if (_veiculoMock != null)
                return _veiculoMock.Where(veiculoMock => veiculoMock.Placa.Trim().ToLower() == placa?.Trim()?.ToLower()).FirstOrDefault();
            else
                return null;
        }

        public List<Veiculo> ListarDisponivel()
        {
            if (_veiculoMock != null)
                return _veiculoMock.Where(veiculoMock => !veiculoMock.EstaAlugado).ToList();
            else
                return null;
        } 

        public List<Veiculo> ListarPorCategoria(CategoriaVeiculo categoria)
        {
            if (_veiculoMock != null)
                return _veiculoMock.Where(veiculoMock => veiculoMock.Categoria == categoria).ToList();
            else
                return null;
        }

        public List<Veiculo> ListarPorCombustivel(TipoCombustivel combustivel)
        {
            if (_veiculoMock != null)
                return _veiculoMock.Where(veiculoMock => veiculoMock.Combustivel == combustivel).ToList();
            else
                return null;
        }

    }
}
