using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.API.Util;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoCheckListDevolucao.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/checklistdevolucao")]
    [ApiController]
    public class CheckListDevolucaoController : ControllerBase
    {
        private readonly ICheckListDevolucaoRepository _checkListDevolucaoRepositorio;
        private readonly IMapper _mapper;

        public CheckListDevolucaoController(ICheckListDevolucaoRepository checkListDevolucaoRepositorio, IMapper mapper)
        {
            _checkListDevolucaoRepositorio = checkListDevolucaoRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações do CheckListDevolucao a partir de seu Id.
        /// </summary>
        /// <param name="id"> Identificador do checkListDevolucao</param>
        /// <returns>Objeto contendo informações do CheckListDevolucao.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(CheckListDevolucaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                CheckListDevolucao checkListDevolucao = _checkListDevolucaoRepositorio.Obter(id);

                if (checkListDevolucao == null)
                    return NotFound(Constantes.Mensagens.CheckListDevolucaoNaoEncontrado);

                return Ok(_mapper.Map<CheckListDevolucao, CheckListDevolucaoModel>(checkListDevolucao));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Obter CheckList Devolução a partir do Identificador do Aluguel.
        /// </summary>
        /// <param name="idAluguel"> Identificador do Aluguel</param>
        /// <returns>Objeto contendo informações do CheckListDevolucao.</returns>
        [HttpGet("{idAluguel}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(CheckListDevolucaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult ObterPorAluguel(int idAluguel)
        {
            try
            {
                CheckListDevolucao checkListDevolucao = _checkListDevolucaoRepositorio.ObterPorAluguel(idAluguel);

                if (checkListDevolucao == null)
                    return NotFound(Constantes.Mensagens.CheckListDevolucaoNaoEncontrado);

                return Ok(_mapper.Map<CheckListDevolucao, CheckListDevolucaoModel>(checkListDevolucao));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo checkListDevolucao.
        /// </summary>
        /// <param name="CheckListDevolucao"> Modelo com informações do checkListDevolucao</param>
        [HttpPost()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(CheckListDevolucaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CheckListDevolucaoModel checkListDevolucaoModelInsersao)
        {
            try
            {
                CheckListDevolucao checkListDevolucaoRequisicaoPost = _mapper.Map<CheckListDevolucaoModel, CheckListDevolucao>(checkListDevolucaoModelInsersao);

                if (checkListDevolucaoRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(checkListDevolucaoRequisicaoPost.Notifications));

                _checkListDevolucaoRepositorio.Inserir(checkListDevolucaoRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do checkListDevolucao
        /// </summary>
        /// <param name="CheckListDevolucao"> Objeto contendo dados do checkListDevolucao</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(CheckListDevolucaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] CheckListDevolucaoModel checkListDevolucaoAtualizacao)
        {
            try
            {
                CheckListDevolucao checkListDevolucaoRequisicaoPut = _mapper.Map<CheckListDevolucaoModel, CheckListDevolucao>(checkListDevolucaoAtualizacao);

                if (checkListDevolucaoRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(checkListDevolucaoRequisicaoPut.Notifications));

                var checkListDevolucaoExistente = _checkListDevolucaoRepositorio.Obter(checkListDevolucaoRequisicaoPut.IdCheckListDevolucao);

                if (checkListDevolucaoExistente != null)
                    _checkListDevolucaoRepositorio.Atualizar(checkListDevolucaoRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.CheckListDevolucaoNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta um CheckList Devolucao existente a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador do checkListDevolucao</param>
        /// 
        [HttpDelete("{id}")]
        [Authorize(Roles = "CheckListDevolucao")]
        [ProducesResponseType(typeof(CheckListDevolucaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int idCheckListDevolucao)
        {
            try
            {
                var checkListDevolucaoExistente = _checkListDevolucaoRepositorio.Obter(idCheckListDevolucao);

                if (checkListDevolucaoExistente != null)
                    _checkListDevolucaoRepositorio.Deletar(idCheckListDevolucao);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.CheckListDevolucaoNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}
