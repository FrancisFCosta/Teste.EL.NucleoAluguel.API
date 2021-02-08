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

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/alugueis")]
    [ApiController]
    public class AluguelController : ControllerBase
    {
        private readonly IAluguelRepository _aluguelRepositorio;
        private readonly IMapper _mapper;

        public AluguelController(IAluguelRepository aluguelRepositorio, IMapper mapper)
        {
            _aluguelRepositorio = aluguelRepositorio;
            _mapper = mapper;
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
        [HttpGet("{idCliente}")]
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
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] AluguelModel aluguelModelInsersao)
        {
            try
            {
                Aluguel aluguelRequisicaoPost = _mapper.Map<AluguelModel, Aluguel>(aluguelModelInsersao);

                if (aluguelRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(aluguelRequisicaoPost.Notifications));

                _aluguelRepositorio.Inserir(aluguelRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do aluguel
        /// </summary>
        /// <param name="Aluguel"> Objeto contendo dados do aluguel</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(AluguelModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] AluguelModel aluguelAtualizacao)
        {
            try
            {
                Aluguel aluguelRequisicaoPut = _mapper.Map<AluguelModel, Aluguel>(aluguelAtualizacao);

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
    }
}
