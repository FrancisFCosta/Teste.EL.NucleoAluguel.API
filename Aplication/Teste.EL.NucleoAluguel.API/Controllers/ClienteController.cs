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
    [Route("api/v1/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepositorio;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository clienteRepositorio, IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações do Cliente a partir de seu CPF.
        /// </summary>
        /// <param name="cpf"> CPF do cliente</param>
        /// <returns>Objeto contendo informações do Cliente.</returns>
        [HttpGet("{cpf}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string cpf)
        {
            try
            {
                Cliente cliente = _clienteRepositorio.ObterPorCPF(cpf);

                if (cliente != null)
                {
                    var clienteModel = _mapper.Map<Cliente, ClienteModel>(cliente);

                    if (cliente.IdEndereco.HasValue)
                    {
                        var endereco = _enderecoRepository.Obter(cliente.IdEndereco.Value);
                        if (endereco != null)
                            clienteModel.Endereco = _mapper.Map<Endereco, EnderecoModel>(endereco);
                    }
                    return Ok(clienteModel);
                }
                else
                {
                    return NotFound(Constantes.Mensagens.ClienteNaoEncontrado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="Cliente"> Modelo com informações do cliente</param>
        [HttpPost()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] ClienteModel clienteModelInsersao)
        {
            try
            {
                Cliente clienteRequisicaoPost = _mapper.Map<ClienteModel, Cliente>(clienteModelInsersao);
                if (clienteRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(clienteRequisicaoPost.Notifications));

                var clienteExistente = _clienteRepositorio.ObterPorCPF(clienteRequisicaoPost.CPF);
                if (clienteExistente != null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, Constantes.Mensagens.CPFUtilizadoPorClienteExistente);

                if (clienteModelInsersao.Endereco != null)
                {
                    Endereco enderecoRequisicaoPost = _mapper.Map<EnderecoModel, Endereco>(clienteModelInsersao.Endereco);

                    if (enderecoRequisicaoPost.Invalid)
                        return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(enderecoRequisicaoPost.Notifications));

                    clienteRequisicaoPost.IdEndereco = _enderecoRepository.Inserir(enderecoRequisicaoPost);
                }

                clienteRequisicaoPost.IdCliente = _clienteRepositorio.Inserir(clienteRequisicaoPost);

                return Ok(clienteRequisicaoPost);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do cliente.
        /// </summary>
        /// <param name="Cliente"> Objeto contendo dados do cliente</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] ClienteModel clienteAtualizacao)
        {
            try
            {
                Cliente clienteRequisicaoPut = _mapper.Map<ClienteModel, Cliente>(clienteAtualizacao);

                if (clienteRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(clienteRequisicaoPut.Notifications));

                var clienteExistente = _clienteRepositorio.Obter(clienteRequisicaoPut.IdCliente);

                if (clienteExistente != null)
                {
                    if (clienteAtualizacao.Endereco != null)
                    {
                        Endereco enderecoRequisicaoPost = _mapper.Map<EnderecoModel, Endereco>(clienteAtualizacao.Endereco);

                        if (enderecoRequisicaoPost.Invalid)
                            return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(enderecoRequisicaoPost.Notifications));

                        _enderecoRepository.Atualizar(enderecoRequisicaoPost);
                    }

                    _clienteRepositorio.Atualizar(clienteRequisicaoPut);
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.ClienteNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta um cliente existente a partir de seu identificador.
        /// </summary>
        /// <param name="id"> Identificador do cliente</param>
        /// 
        [HttpDelete("{id}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var clienteExistente = _clienteRepositorio.Obter(id);

                if (clienteExistente != null) 
                {
                    if (clienteExistente.IdEndereco.HasValue)
                        _enderecoRepository.Deletar(clienteExistente.IdEndereco.Value);

                    _clienteRepositorio.Deletar(id);
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.ClienteNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}
