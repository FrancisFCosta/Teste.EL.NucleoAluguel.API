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
    [Route("api/v1/marcas")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaRepository _marcaRepositorio;
        private readonly IMapper _mapper;

        public MarcaController(IMarcaRepository marcaRepositorio, IMapper mapper)
        {
            _marcaRepositorio = marcaRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações da Marca a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador da marca</param>
        /// <returns>Objeto contendo informações da Marca.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                Marca marca = _marcaRepositorio.Obter(id);

                if (marca == null)
                    return NotFound(Constantes.Mensagens.MarcaNaoEncontrada);

                return Ok(_mapper.Map<Marca, MarcaModel>(marca));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Lista as Marcas cadastrados.
        /// </summary>
        /// <returns>Lista contendo informações das marcas cadastrados.</returns>
        [HttpGet()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                List<Marca> listaMarca = _marcaRepositorio.Listar();
                return Ok(_mapper.Map< List<Marca>, List<MarcaModel>>(listaMarca ?? new List<Marca>()));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria uma nova marca.
        /// </summary>
        /// <param name="Marca"> Model com informações da marca de veículo</param>
        [HttpPost()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] MarcaModel marcaInsersao)
        {
            try
            {
                Marca marcaRequisicaoPost = _mapper.Map<MarcaModel, Marca>(marcaInsersao);

                if (marcaRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(marcaRequisicaoPost.Notifications));

                var marcaExistente = _marcaRepositorio.Obter(marcaRequisicaoPost.IdMarca);

                if (marcaExistente != null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, Constantes.Mensagens.IdUtilizadoPorMarcaExistente);
                else
                    _marcaRepositorio.Inserir(marcaRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações da marca.
        /// </summary>
        /// <param name="Marca"> Objeto contendo dados da marca</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] MarcaModel marcaAtualizacao)
        {
            try
            {
                Marca marcaRequisicaoPut = _mapper.Map<MarcaModel, Marca>(marcaAtualizacao);

                if (marcaRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(marcaRequisicaoPut.Notifications));

                var marcaExistente = _marcaRepositorio.Obter(marcaRequisicaoPut.IdMarca);

                if (marcaExistente != null)
                    _marcaRepositorio.Atualizar(marcaRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.MarcaNaoEncontrada);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta uma marca existente a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador da marca</param>
        /// 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var marcaExistente = _marcaRepositorio.Obter(id);

                if (marcaExistente != null)
                    _marcaRepositorio.Deletar(id);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.MarcaNaoEncontrada);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}
