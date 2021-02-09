using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.API.Util;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/v1/operadores")]
    [ApiController]
    public class OperadorController : ControllerBase
    {
        private readonly IOperadorRepository _operadorRepositorio;
        private readonly IMapper _mapper;

        public OperadorController(IOperadorRepository operadorRepositorio, IMapper mapper)
        {
            _operadorRepositorio = operadorRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações do Operador a partir de sua Matrícula.
        /// </summary>
        /// <param name="matricula"> Matrícula do operador</param>
        /// <returns>Objeto contendo informações do Operador.</returns>
        [HttpGet("{matricula}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string matricula)
        {
            try
            {
                Operador operador = _operadorRepositorio.Obter(matricula);

                if (operador == null)
                    return NotFound(Constantes.Mensagens.OperadorNaoEncontrado);

                return Ok(_mapper.Map<Operador, OperadorModel>(operador));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo operador.
        /// </summary>
        /// <param name="Operador"> Modelo com informações do operador</param>
        [HttpPost()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] OperadorModel operadorModelInsersao)
        {
            try
            {
                Operador operadorRequisicaoPost = _mapper.Map<OperadorModel, Operador>(operadorModelInsersao);

                if (operadorRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(operadorRequisicaoPost.Notifications));

                var operadorExistente = _operadorRepositorio.Obter(operadorRequisicaoPost.Matricula);

                if (operadorExistente != null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, Constantes.Mensagens.MatriculaUtilizadoPorOperadorExistente);
                else
                    _operadorRepositorio.Inserir(operadorRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do operador.
        /// </summary>
        /// <param name="Operador"> Objeto contendo dados do operador</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] OperadorModel operadorAtualizacao)
        {
            try
            {
                Operador operadorRequisicaoPut = _mapper.Map<OperadorModel, Operador>(operadorAtualizacao);

                if (operadorRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(operadorRequisicaoPut.Notifications));

                var operadorExistente = _operadorRepositorio.Obter(operadorRequisicaoPut.Matricula);

                if (operadorExistente != null)
                    _operadorRepositorio.Atualizar(operadorRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.OperadorNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta um operador existente a partir de sua Matricula.
        /// </summary>
        /// <param name="matricula"> Matricula do operador</param>
        /// 
        [HttpDelete("{matricula}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string matricula)
        {
            try
            {
                var operadorExistente = _operadorRepositorio.Obter(matricula);

                if (operadorExistente != null)
                    _operadorRepositorio.Deletar(matricula);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.OperadorNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}
