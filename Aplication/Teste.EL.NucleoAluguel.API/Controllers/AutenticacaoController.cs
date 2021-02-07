﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.EL.NucleoAluguel.API.Auth;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly IMapper _mapper;
        public AutenticacaoController(IUsuarioRepository usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="Usuario"> Modelo com informações do usuário</param>
        [HttpPost()]
        [Route("Autenticar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public ActionResult<dynamic> Authenticate([FromBody] UsuarioModel usuarioModelInsersao)
        {
            try
            {
                Usuario usuarioConversao = _mapper.Map<UsuarioModel, Domain.Entities.Usuario>(usuarioModelInsersao);

                if (usuarioConversao != null)
                {
                    if (usuarioConversao.Invalid)
                        return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(usuarioConversao.Notifications));

                    var usuarioExistente = _usuarioRepositorio.ObterPorLoginESenha(usuarioConversao.Login, usuarioConversao.Senha);

                    if (usuarioExistente != null)
                    {
                        var token = TokenService.GenerateToken(usuarioExistente);
                        var usuarioModel = _mapper.Map<Domain.Entities.Usuario, UsuarioModel>(usuarioExistente);
                        usuarioModel.Senha = String.Empty;
                        return new
                        {
                            usuario = usuarioModel,
                            token = token
                        };
                    }
                    else
                        return NotFound("Usuário ou senha inválidos");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "O Serviço está temporariamente indisponível."); throw;
            }

            return Ok();
        }
    }
}
