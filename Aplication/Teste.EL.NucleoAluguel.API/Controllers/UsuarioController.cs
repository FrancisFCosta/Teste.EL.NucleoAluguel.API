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
    [Route("api/v1/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações do usuário a partir de seu Id. Por motivos de segurança a senha não será preenchida no model de retorno da consulta.
        /// </summary>
        /// <param name="id"> Identificador do usuário</param>
        /// <returns>Objeto contendo informações do usuário.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                Usuario usuario = _usuarioRepositorio.Obter(id);

                if (usuario == null)
                    return NotFound(Constantes.Mensagens.UsuarioNaoEncontrado);

                return Ok(_mapper.Map<Usuario, UsuarioModel>(usuario));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="Usuario"> Modelo com informações do usuário</param>
        [HttpPost()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] UsuarioModel usuarioModelInsersao)
        {
            try
            {
                Usuario usuarioRequisicaoPost = _mapper.Map<UsuarioModel, Usuario>(usuarioModelInsersao);

                if (usuarioRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(usuarioRequisicaoPost.Notifications));

                var usuarioExistente = _usuarioRepositorio.ObterPorLogin(usuarioRequisicaoPost.Login);

                if (usuarioExistente != null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, Constantes.Mensagens.LoginUtilizadoPorUsuarioExistente);
                else
                    _usuarioRepositorio.Inserir(usuarioRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do usuário.
        /// </summary>
        /// <param name="Usuario"> Objeto contendo dados do usuário</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] UsuarioModel usuarioAtualizacao)
        {
            try
            {
                Usuario usuarioRequisicaoPut = _mapper.Map<UsuarioModel, Usuario>(usuarioAtualizacao);

                if (usuarioRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(usuarioRequisicaoPut.Notifications));

                var usuarioExistente = _usuarioRepositorio.Obter(usuarioRequisicaoPut.IdUsuario);

                if (usuarioExistente != null)
                    _usuarioRepositorio.Atualizar(usuarioRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.UsuarioNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta um usuário existente a partir de seu Identificador.
        /// </summary>
        /// <param name="id"> Identificador do usuário</param>
        /// 
        [HttpDelete()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var usuarioExistente = _usuarioRepositorio.Obter(id);

                if (usuarioExistente != null)
                    _usuarioRepositorio.Deletar(id);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.UsuarioNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}
