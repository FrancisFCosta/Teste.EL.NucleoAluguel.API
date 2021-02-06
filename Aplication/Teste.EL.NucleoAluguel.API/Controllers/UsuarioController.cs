using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/[controller]")]
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
        /// Obtém informações do usuário a partir de seu Id
        /// </summary>
        /// <param name="Id"> Id do usuário</param>
        /// <returns>Objeto contendo informações do usuário</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado é inválido");

            Domain.Entities.Usuario usuario;

            try
            {
                usuario = _usuarioRepositorio.Obter(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "O Serviço está temporariamente indisponível.");
            }

            if (usuario == null)
                return NotFound("Usuário não encontrado");

            return Ok(_mapper.Map<Domain.Entities.Usuario, UsuarioModel>(usuario));
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="Usuario"> Modelo com informações do usuário</param>
        [HttpPost("{Usuario}")]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] UsuarioModel usuarioModelInsersao)
        {
            try
            {
                Usuario usuarioConversao = _mapper.Map<UsuarioModel, Domain.Entities.Usuario>(usuarioModelInsersao);
                if (usuarioConversao != null)
                    _usuarioRepositorio.Inserir(usuarioConversao);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "O Serviço está temporariamente indisponível."); throw;
            }

            return Ok();
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{Usuario}")]
        public void Put([FromBody] UsuarioModel value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
