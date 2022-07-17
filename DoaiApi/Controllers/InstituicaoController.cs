using AutoMapper;
using DoaiApi.Data;
using DoaiApi.Data.DTOs;
using DoaiApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DoaiApi.Controllers
{
    [ApiController]
    [Route("Doai/[controller]")]
    public class InstituicaoController : ControllerBase
    {
        private InstituicaoContext _context;
        private IMapper _mapper;

        /// <summary>
        /// Get Database objetos
        /// </summary>
        public InstituicaoController(InstituicaoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        /// <summary>
        /// Metodo para Adicionar uma nova institução
        /// </summary>
        /// <param CreateInstituicaoDto="instituicaoDto"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso: Instituição cadastrada</response>
        /// <response code="401">Erro: Usuario nao autenticado</response>
        [HttpPost]
        [Authorize]
        public IActionResult AdicionaInstituicao([FromBody] CreateInstituicaoDto instituicaoDto)
        {
            Instituicao instituicao = _mapper.Map<Instituicao>(instituicaoDto);
            _context.Instituicoes.Add(instituicao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaInstituicaoPorId), new { Id = instituicao.Id }, instituicao);
        }


        /// <summary>
        /// Metodo traz uma lista de instuições
        /// </summary>
        /// <response code="200">Sucesso: Retorna lista</response>
        /// <response code="401">Erro: Usuario nao autenticado</response>
        [Route("ListarInstituicoes")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Instituicao> RecuperaInstituicoes()
        {
            return _context.Instituicoes;
        }

        /// <summary>
        /// Metodo para retornar uma institução por seu id
        /// </summary>
        /// <param int="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso: Instituição cadastrada</response>
        /// <response code="401">Erro: Usuario nao autenticado</response>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult RecuperaInstituicaoPorId(int id)
        {
            Instituicao instituicao = _context.Instituicoes.FirstOrDefault(instituicao => instituicao.Id == id);
            if (instituicao != null)
            {

                return Ok(instituicao);
            }
            return NotFound();
        }
    }
}
