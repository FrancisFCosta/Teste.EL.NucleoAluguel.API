using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Teste.EL.NucleoAluguel.API.Models;
using Teste.EL.NucleoAluguel.API.Util;
using Teste.EL.NucleoAluguel.Domain.Entities;
using Teste.EL.NucleoAluguel.Domain.Repositories;

namespace Teste.EL.NucleoAluguel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAluguelRepository _aluguelRepositorio;

        public ContratoController(IAluguelRepository aluguelRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _aluguelRepositorio = aluguelRepositorio;
        }

        /// <summary>
        /// Gera contrato de alguel com base em seu ID.
        /// </summary>
        /// <returns>Arquivo contendo o contrato de Aluguel.</returns>
        [HttpGet("{idAluguel}")]
        //[Authorize(Roles = "Operador")]
        [ProducesResponseType(typeof(ContratoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public ActionResult<dynamic> Get(int idAluguel)
        {
            try
            {
                var aluguel = _aluguelRepositorio.Obter(idAluguel);

                if (aluguel != null)
                {
                    MemoryStream vFileStream = GeradorArquivos.GerarContratoAluguel(_mapper.Map<Aluguel, AluguelModel>(aluguel));
                    return File(vFileStream, "application/pdf", $"ContratoAluguel_{aluguel.IdAluguel}.pdf");
                }
                else
                {
                    return NotFound(Constantes.Mensagens.AluguelNaoEncontrado);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }

        /// <summary>
        /// Obtém modelo de contrato em pdf.
        /// </summary>
        /// <returns>Arquivo contendo modelo do contrato de Aluguel.</returns>
        [HttpGet()]
        [Route("Modelo")]
        //[Authorize(Roles = "Operador, Cliente")]
        [ProducesResponseType(typeof(ContratoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public ActionResult<dynamic> Get()
        {
            try
            {
                System.IO.FileStream vFileStream = GeradorArquivos.ObterModeloContrato();
                return File(vFileStream, "application/pdf", "ContratoAluguel.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Constantes.Mensagens.ServicoIndisponivel);
            }
        }
    }
}
