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
using Teste.EL.NucleoAluguel.Domain.Enums;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/v1/veiculos")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepositorio;
        private readonly IMapper _mapper;

        public VeiculoController(IVeiculoRepository veiculoRepositorio, IMapper mapper)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém informações da Veiculo a partir de sua Placa.
        /// </summary>
        /// <param name="id"> Identificador da veiculo</param>
        /// <returns>Objeto contendo informações da Veiculo.</returns>
        [HttpGet("{placa}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string placa)
        {
            try
            {
                Veiculo veiculo = _veiculoRepositorio.ObterPorPlaca(placa);

                if (veiculo == null)
                    return NotFound(Constantes.Mensagens.VeiculoNaoEncontrado);

                return Ok(_mapper.Map<Veiculo, VeiculoModel>(veiculo));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Lista veículos disponíveis para aluguel.
        /// </summary>
        /// <returns>Lista de veículos disponíveis para aluguel.</returns>
        [HttpGet()]
        [Route("disponiveis")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult ListarDisponiveis()
        {
            try
            {
                List<Veiculo> listaVeiculos = _veiculoRepositorio.ListarDisponivel();
                List<VeiculoModel> listaModel = listaVeiculos != null && listaVeiculos.Any() ? _mapper.Map<List<Veiculo>, List<VeiculoModel>>(listaVeiculos) : new List<VeiculoModel>();

                return Ok(listaModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Lista veículos por categoria. Valores de referência: (1)Básico; (2)Completo; (3) Luxo; 
        /// </summary>
        /// <param name="categoria"> codigo de categoria do veículo</param>
        /// <returns>Objeto contendo informações da Veiculo.</returns>
        [HttpGet("{categoria}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(CategoriaVeiculo categoria)
        {
            try
            {
                List<Veiculo> listaVeiculos = _veiculoRepositorio.ListarPorCategoria(categoria);
                List<VeiculoModel> listaModel = listaVeiculos != null && listaVeiculos.Any() ?_mapper.Map<List<Veiculo>, List<VeiculoModel>>(listaVeiculos) : new List<VeiculoModel>();

                return Ok(listaModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Lista veículos por tipo de combustível. Valores de referência: (1)Álcool; (2)Gasolina; (3) Diesel; 
        /// </summary>
        /// <param name="combustivel"> codigo de tipo de combustivel do veículo</param>
        /// <returns>Objeto contendo informações da Veiculo.</returns>
        [HttpGet("{combustivel}")]
        [Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(TipoCombustivel combustivel)
        {
            try
            {
                List<Veiculo> listaVeiculos = _veiculoRepositorio.ListarPorCombustivel(combustivel);
                List<VeiculoModel> listaModel = listaVeiculos != null && listaVeiculos.Any() ? _mapper.Map<List<Veiculo>, List<VeiculoModel>>(listaVeiculos) : new List<VeiculoModel>();

                return Ok(listaModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Cria um novo veiculo.
        /// </summary>
        /// <param name="Veiculo"> Model com informações da veiculo de veículo</param>
        [HttpPost()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] VeiculoModel veiculoInsersao)
        {
            try
            {
                Veiculo veiculoRequisicaoPost = _mapper.Map<VeiculoModel, Veiculo>(veiculoInsersao);

                if (veiculoRequisicaoPost.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(veiculoRequisicaoPost.Notifications));

                var veiculoExistente = _veiculoRepositorio.Obter(veiculoRequisicaoPost.IdVeiculo);

                if (veiculoExistente != null)
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, Constantes.Mensagens.PlacaUtilizadoPorVeiculoExistente);
                else
                    _veiculoRepositorio.Inserir(veiculoRequisicaoPost);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Atualiza informações do veiculo.
        /// </summary>
        /// <param name="Veiculo"> Objeto contendo dados da veiculo</param>
        /// 
        [HttpPut()]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] VeiculoModel veiculoAtualizacao)
        {
            try
            {
                Veiculo veiculoRequisicaoPut = _mapper.Map<VeiculoModel, Veiculo>(veiculoAtualizacao);

                if (veiculoRequisicaoPut.Invalid)
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorModel(veiculoRequisicaoPut.Notifications));

                var veiculoExistente = _veiculoRepositorio.Obter(veiculoRequisicaoPut.IdVeiculo);

                if (veiculoExistente != null)
                    _veiculoRepositorio.Atualizar(veiculoRequisicaoPut);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.VeiculoNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }

        /// <summary>
        /// Deleta uma veiculo existente a partir de sua Placa.
        /// </summary>
        /// <param name="id"> Identificador da veiculo</param>
        /// 
        [HttpDelete("{placa}")]
        [Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string placa)
        {
            try
            {
                var veiculoExistente = _veiculoRepositorio.ObterPorPlaca(placa);

                if (veiculoExistente != null)
                    _veiculoRepositorio.Deletar(placa);
                else
                    return StatusCode(StatusCodes.Status404NotFound, Constantes.Mensagens.VeiculoNaoEncontrado);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel); throw;
            }
        }
    }
}

