using System;
using System.Collections.Generic;
using System.Text;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.Domain.Services
{
    public class DevolucaoService
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ICheckListDevolucaoRepository _checkListDevolucaoRepository;

        public DevolucaoService(IAluguelRepository aluguelRepository, IVeiculoRepository veiculoRepository, ICheckListDevolucaoRepository checkListDevolucaoRepository)
        {
            _aluguelRepository = aluguelRepository;
            _veiculoRepository = veiculoRepository;
            _checkListDevolucaoRepository = checkListDevolucaoRepository;
        }

        public CheckListDevolucao Devolver(CheckListDevolucao devolucao)
        {
            Veiculo veiculoAluguel = _veiculoRepository.Obter(devolucao.IdVeiculo);
            if (veiculoAluguel == null)
            {
                devolucao.AddNotification(nameof(devolucao.IdVeiculo), $"Veículo [ID:{devolucao.IdVeiculo}] informado para devolução não existe.");
                return devolucao;
            }

            Aluguel aluguel = _aluguelRepository.Obter(devolucao.IdAluguel);
            if (aluguel == null)
            {
                devolucao.AddNotification(nameof(devolucao.IdAluguel), $"Aluguel [ID:{devolucao.IdAluguel}] informado para devolução não existe.");
                return devolucao;
            }

            if (devolucao.Valid)
            {
                devolucao.IdCheckListDevolucao = _checkListDevolucaoRepository.Inserir(devolucao);
                _veiculoRepository.AtualizarDisponibilidade(devolucao.IdVeiculo, false);
            }

            return devolucao;
        }
    }
}
