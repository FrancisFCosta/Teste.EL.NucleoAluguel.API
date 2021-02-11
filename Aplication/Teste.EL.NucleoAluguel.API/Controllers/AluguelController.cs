using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.API.Util;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;
using Teste.EL.NucleoAluguel.Domain.Services;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/v1/alugueis")]
    [ApiController]
    public class AluguelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AluguelService _aluguelService;
        private readonly IAluguelRepository _aluguelRepositorio;
        private readonly IVeiculoRepository _veiculoRepository;

        public AluguelController(IAluguelRepository aluguelRepositorio, IVeiculoRepository veiculoRepository, IMapper mapper, AluguelService aluguelService)
        {
            _mapper = mapper;
            _aluguelService = aluguelService;
            _aluguelRepositorio = aluguelRepositorio;
            _veiculoRepository = veiculoRepository;
        }

        /// <summary>
        /// Obtém informações do Aluguel a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador do aluguel</param>
        /// <returns>Objeto contendo informações do Aluguel.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                Aluguel aluguel = _aluguelRepositorio.Obter(id);

                if (aluguel == null)
                    return NotFound(Constantes.Mensagens.AluguelNaoEncontrado);

                return Ok(_mapper.Map<Aluguel, AluguelModel>(aluguel));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Listar Aluguéis a partir do Identificador do Cliente.
        /// </summary>
        /// <param name="idCliente"> Identificador do aluguel</param>
        /// <returns>Objeto contendo informações do Aluguel.</returns>
        [HttpGet("clientes/{idCliente}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult ListarPorCliente(int idCliente)
        {
            try
            {
                List<Aluguel> listaAlugueisDoCliente = _aluguelRepositorio.ListarPorCliente(idCliente);
                List<AluguelModel> listaAluguelModel = listaAlugueisDoCliente != null && listaAlugueisDoCliente.Any() ?
                    _mapper.Map<List<Aluguel>, List<AluguelModel>>(listaAlugueisDoCliente) : new List<AluguelModel>();
                PreencherVeiculosAluguel(listaAluguelModel);
                return Ok(listaAluguelModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo aluguel.
        /// </summary>
        /// <param name="Aluguel"> Modelo com informações do aluguel</param>
        [HttpPost()]
        [AllowAnonymous]
        [Route("simulacao")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult SimularAluguel([FromBody] AluguelModel aluguel)
        {
            try
            {
                Aluguel aluguelRequisicaoPost = _mapper.Map<AluguelModel, Aluguel>(aluguel);

                if (aluguelRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(aluguelRequisicaoPost.Notifications));

                var retornoSimulacao = _aluguelService.Simular(aluguelRequisicaoPost);

                if (retornoSimulacao.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(retornoSimulacao.Notifications));
                else
                    return Ok(_mapper.Map<Aluguel, AluguelModel>(retornoSimulacao));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Cria um novo aluguel.
        /// </summary>
        /// <param name="Aluguel"> Modelo com informações do aluguel</param>
        [HttpPost()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] AluguelModel aluguel)
        {
            try
            {
                Aluguel aluguelRequisicaoPost = _mapper.Map<AluguelModel, Aluguel>(aluguel);

                if (aluguelRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(aluguelRequisicaoPost.Notifications));

                var retornoAluguel = _aluguelService.Alugar(aluguelRequisicaoPost);

                if (retornoAluguel.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(retornoAluguel.Notifications));
                else
                    return Ok(retornoAluguel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do aluguel.
        /// </summary>
        /// <param name="Aluguel"> Objeto contendo dados do aluguel</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] AluguelModel aluguel)
        {
            try
            {
                Aluguel aluguelRequisicaoPut = _mapper.Map<AluguelModel, Aluguel>(aluguel);

                if (aluguelRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(aluguelRequisicaoPut.Notifications));

                var aluguelExistente = _aluguelRepositorio.Obter(aluguelRequisicaoPut.IdAluguel);

                if (aluguelExistente != null)
                    _aluguelRepositorio.Atualizar(aluguelRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.AluguelNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta um aluguel existente a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador do aluguel</param>
        /// 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Aluguel")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int idAluguel)
        {
            try
            {
                var aluguelExistente = _aluguelRepositorio.Obter(idAluguel);

                if (aluguelExistente != null)
                    _aluguelRepositorio.Deletar(idAluguel);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.AluguelNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        private void PreencherVeiculosAluguel(List<AluguelModel> listaAluguelModel)
        {
            if (listaAluguelModel != null)
            {
                foreach (var aluguel in listaAluguelModel)
                {
                    var veiculo = _veiculoRepository.Obter(aluguel.Veiculo.IdVeiculo);
                    aluguel.Veiculo = _mapper.Map<Veiculo, VeiculoModel>(veiculo);
                }
            }
        }
    }
}
