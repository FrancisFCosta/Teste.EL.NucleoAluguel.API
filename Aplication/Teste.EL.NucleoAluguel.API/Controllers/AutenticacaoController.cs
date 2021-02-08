using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Teste.EL.NucleoAluguel.API.Auth;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.API.Util;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly IOperadorRepository _operadorRepositorio;
        private readonly IClienteRepository _clienteRepositorio;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        public AutenticacaoController(IUsuarioRepository usuarioRepositorio, IOperadorRepository operadorRepositorio, IClienteRepository clienteRepositorio,
            IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _operadorRepositorio = operadorRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Autentica o usuário, possibilitando acesso à outros serviços da solução.
        /// </summary>
        /// <param name="Usuario"> Modelo com informações do usuário para autenticação</param>
        /// <returns>Objeto contendo informações do usuário e o Token para utilização.</returns>
        [HttpPost()]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public ActionResult<dynamic> Post([FromBody] UsuarioModel usuarioModelInsersao)
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

                        if (Formatacao.isCpf(usuarioExistente.Login) || usuarioExistente.Perfil == Domain.Enums.PerfilUsuario.Cliente)
                        {
                            var cliente = _clienteRepositorio.ObterPorCPF(usuarioExistente.Login);
                            var clienteModel = _mapper.Map<Cliente, ClienteModel>(cliente);

                            if (cliente.IdEndereco.HasValue)
                            {
                                var endereco = _enderecoRepository.Obter(cliente.IdEndereco.Value);
                                if (endereco != null)
                                    clienteModel.Endereco = _mapper.Map<Endereco, EnderecoModel>(endereco);
                            }

                            return new
                            {
                                usuario = usuarioModel,
                                cliente = clienteModel,
                                token = token
                            };
                        }
                        else
                        {
                            var operador = _operadorRepositorio.Obter(usuarioExistente.Login);
                            return new
                            {
                                usuario = usuarioModel,
                                operador = _mapper.Map<Domain.Entities.Operador, OperadorModel>(operador),
                                token = token
                            };
                        }
                    }
                    else
                        return NotFound("Usuário ou senha inválidos");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "O Serviço está temporariamente indisponível."); throw;
            }

        }
    }
}
