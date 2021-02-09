using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.Domain.Services
{
    public class AluguelService
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IClienteRepository _clienteRepository;

        public AluguelService(IAluguelRepository aluguelRepository, IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository)
        {
            _aluguelRepository = aluguelRepository;
            _veiculoRepository = veiculoRepository;
            _clienteRepository = clienteRepository;
        }

        public Aluguel Simular(Aluguel dadosAluguel)
        {
            Aluguel simulacao = dadosAluguel;

            Veiculo veiculoAluguel = _veiculoRepository.Obter(dadosAluguel.IdVeiculo);
            if (veiculoAluguel == null)
            {
                simulacao.AddNotification(nameof(dadosAluguel.IdVeiculo), $"Veículo [ID:{dadosAluguel.IdVeiculo}] informado para aluguel não existe.");
                return simulacao;
            }

            Cliente clienteAluguel = _clienteRepository.Obter(dadosAluguel.IdCliente);
            if (clienteAluguel == null)
            {
                simulacao.AddNotification(nameof(dadosAluguel.IdVeiculo), $"Cliente [ID:{dadosAluguel.IdVeiculo}] informado para aluguel não existe.");
                return simulacao;
            }

            simulacao.ValorFinal = veiculoAluguel.ValorHora * simulacao.TotalDeHoras;

            return simulacao;
        }

        public Aluguel Alugar(Aluguel dadosAluguel)
        {
            Aluguel aluguelParaProcessamento = Simular(dadosAluguel);
            if (aluguelParaProcessamento.Valid)
            {
                aluguelParaProcessamento.IdAluguel = _aluguelRepository.Inserir(aluguelParaProcessamento);
                _veiculoRepository.AtualizarDisponibilidade(aluguelParaProcessamento.IdVeiculo, true);
            }

            return aluguelParaProcessamento;
        }
    }
}
