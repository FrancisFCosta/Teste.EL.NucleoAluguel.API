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
        private readonly IMarcaRepository _marcaRepository;
        private readonly IModeloRepository _modeloRepository;

        public VeiculoController(IVeiculoRepository veiculoRepositorio, IMarcaRepository marcaRepository, IModeloRepository modeloRepository, IMapper mapper)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _mapper = mapper;
            _marcaRepository = marcaRepository;
            _modeloRepository = modeloRepository;
        }

        /// <summary>
        /// Obtém informações da Veiculo a partir de sua Placa.
        /// </summary>
        /// <param name="id"> Identificador da veiculo</param>
        /// <returns>Objeto contendo informações da Veiculo.</returns>
        [AllowAnonymous]
        [HttpGet()]
        [Route("{placa}")]
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
        /// Lista veículos por categoria. Valores de referência: (1)Básico; (2)Completo; (3) Luxo; 
        /// </summary>
        /// <returns>Objeto contendo informações da Veiculo.</returns>
        [AllowAnonymous]
        [HttpGet("categoria")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult ListarPorCategoria([FromQuery] CategoriaVeiculo categoria)
        {
            try
            {
                List<Veiculo> listaVeiculos = _veiculoRepositorio.ListarPorCategoria(categoria);
                List<VeiculoModel> listaVeiculoModel = listaVeiculos != null && listaVeiculos.Any() ? _mapper.Map<List<Veiculo>, List<VeiculoModel>>(listaVeiculos) : new List<VeiculoModel>();

                if (listaVeiculos != null)
                {
                    PreencherModelo(listaVeiculos, listaVeiculoModel);
                    PreencherMarcas(listaVeiculos, listaVeiculoModel);
                }

                return Ok(listaVeiculoModel);
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
        [AllowAnonymous]
        [Route("disponiveis")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public IActionResult ListarDisponiveis()
        {
            try
            {
                List<Veiculo> listaVeiculos = _veiculoRepositorio.ListarDisponivel();
                List<VeiculoModel> listaVeiculoModel = listaVeiculos != null && listaVeiculos.Any() ? _mapper.Map<List<Veiculo>, List<VeiculoModel>>(listaVeiculos) : new List<VeiculoModel>();

                if (listaVeiculos != null)
                {
                    PreencherModelo(listaVeiculos, listaVeiculoModel);
                    PreencherMarcas(listaVeiculos, listaVeiculoModel);
                }

                return Ok(listaVeiculoModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        private void PreencherMarcas(List<Veiculo> listaVeiculos, List<VeiculoModel> listaVeiculoModel)
        {
            var modelos = listaVeiculos.Select(v => v.IdMarca)?.Distinct()?.ToList();
            List<ModeloModel> listaModelos = new List<ModeloModel>();
            if (modelos != null && modelos.Any())
            {
                foreach (var modelo in modelos)
                {
                    var modeloRecuperado = _modeloRepository.Obter(modelo);
                    if (modeloRecuperado != null)
                        listaModelos.Add(_mapper.Map<Modelo, ModeloModel>(modeloRecuperado));
                }
            }
            listaVeiculoModel.ForEach(veiculo =>
            {
                veiculo.Modelo = listaModelos.Where(m => m.IdModelo == veiculo.Modelo.IdModelo).FirstOrDefault();
            });
        }

        private void PreencherModelo(List<Veiculo> listaVeiculos, List<VeiculoModel> listaVeiculoModel)
        {
            var marcas = listaVeiculos.Select(v => v.IdMarca)?.Distinct()?.ToList();
            List<MarcaModel> listaMarcas = new List<MarcaModel>();
            if (marcas != null && marcas.Any())
            {
                foreach (var marca in marcas)
                {
                    var marcaRecuperada = _marcaRepository.Obter(marca);
                    if (marcaRecuperada != null)
                        listaMarcas.Add(_mapper.Map<Marca, MarcaModel>(marcaRecuperada));
                }
            }

            listaVeiculoModel.ForEach(veiculo =>
            {
                veiculo.Marca = listaMarcas.Where(m => m.IdMarca == veiculo.Marca.IdMarca).FirstOrDefault();
            });
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

