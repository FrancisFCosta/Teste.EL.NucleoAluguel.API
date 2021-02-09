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
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/v1/modelos")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly IModeloRepository _modeloRepositorio;
        private readonly IMapper _mapper;

        public ModeloController(IModeloRepository modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações do Modelo a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador do modelo</param>
        /// <returns>Objeto contendo informações do Modelo.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                Modelo modelo = _modeloRepositorio.Obter(id);

                if (modelo == null)
                    return NotFound(Constantes.Mensagens.ModeloNaoEncontrado);

                return Ok(_mapper.Map<Modelo, ModeloModel>(modelo));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Lista os Modelos cadastrados.
        /// </summary>
        /// <returns>Lista contendo informações dos modelos cadastrados.</returns>
        [HttpGet()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                List<Modelo> listaModelo = _modeloRepositorio.Listar();
                return Ok(_mapper.Map< List<Modelo>, List<ModeloModel>>(listaModelo ?? new List<Modelo>()));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo modelo.
        /// </summary>
        /// <param name="Modelo"> Model com informações do modelo de veículo</param>
        [HttpPost()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] ModeloModel modeloInsersao)
        {
            try
            {
                Modelo modeloRequisicaoPost = _mapper.Map<ModeloModel, Modelo>(modeloInsersao);

                if (modeloRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(modeloRequisicaoPost.Notifications));

                var modeloExistente = _modeloRepositorio.Obter(modeloRequisicaoPost.IdModelo);

                if (modeloExistente != null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, Constantes.Mensagens.IdUtilizadoPorModeloExistente);
                else
                    _modeloRepositorio.Inserir(modeloRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do modelo.
        /// </summary>
        /// <param name="Modelo"> Objeto contendo dados do modelo</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] ModeloModel modeloAtualizacao)
        {
            try
            {
                Modelo modeloRequisicaoPut = _mapper.Map<ModeloModel, Modelo>(modeloAtualizacao);

                if (modeloRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(modeloRequisicaoPut.Notifications));

                var modeloExistente = _modeloRepositorio.Obter(modeloRequisicaoPut.IdModelo);

                if (modeloExistente != null)
                    _modeloRepositorio.Atualizar(modeloRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.ModeloNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta um modelo existente a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador do modelo</param>
        /// 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var modeloExistente = _modeloRepositorio.Obter(id);

                if (modeloExistente != null)
                    _modeloRepositorio.Deletar(id);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.ModeloNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}
