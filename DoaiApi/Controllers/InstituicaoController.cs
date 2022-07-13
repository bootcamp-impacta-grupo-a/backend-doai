using AutoMapper;
using DoaiApi.Data;
using DoaiApi.Data.DTOs;
using DoaiApi.Models;
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

        public InstituicaoController(InstituicaoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost]
        public IActionResult AdicionaInstituicao([FromBody] CreateInstituicaoDto instituicaoDto)
        {
            Instituicao instituicao = _mapper.Map<Instituicao>(instituicaoDto);
            _context.Instituicoes.Add(instituicao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaInstituicaoPorId), new { Id = instituicao.Id }, instituicao);
        }

        [HttpGet]
        public IEnumerable<Instituicao> RecuperaInstituicoes()
        {
            return _context.Instituicoes;
        }

        [HttpGet("{id}")]
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
