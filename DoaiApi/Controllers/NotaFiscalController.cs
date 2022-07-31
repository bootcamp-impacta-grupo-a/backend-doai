using DoaiApi.Data;
using DoaiApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoaiApi.Controllers
{
    [Route("Doai/[controller]")]
    public class NotaFiscalController : Controller
    {

        private InstituicaoContext _context;

        /// <summary>
        /// Get Database objetos
        /// </summary>
        public NotaFiscalController(InstituicaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna um arquivo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso: Arquivo retornado</response>
        /// <response code="400">Erro: Arquivo não retornado</response>
        [HttpGet]
        [Route("RetornaNota")]
        [AllowAnonymous]
        public IActionResult RetornoArquivo(int id)
        {

            NotaFiscal arquivoRetorno = _context.NotaFiscal.FirstOrDefault(c => c.Id == id);

            if (arquivoRetorno == null)
                return NotFound(new { message = "Não foi encontrado nenhum arquivo" });

            Response.Headers.Add("Content-Disposition", "inline; filename=" + arquivoRetorno.FileName);

            return new FileContentResult(arquivoRetorno.ArrayBytes, arquivoRetorno.ContentType);
        }


        /// <summary>
        /// Método para retorno de notas, por instituição
        /// </summary>
        /// <param name="idInstituicao"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso: Arquivo salvo</response>
        /// <response code="400">Erro: Falha ao enviar arquivo</response>
        /// <response code="401">Erro: Usuário não autenticado ou não autorizado</response>
        [HttpGet]
        [Route("RetornaNotasInstituicao")]
        [AllowAnonymous]
        public IActionResult RetornaNotasByInstituicao (int idInstituicao)
        {

            if (idInstituicao == 0)
                return NotFound(new { message = "Informe o id da instituição" });

            var NotasFiscais = _context.NotaFiscal.Where(a => a.InstituicaoId == idInstituicao).Select(a => new {a.Id, a.FileName, a.DataEnvio }).ToList();

            return Ok(NotasFiscais);
        }

        /// <summary>
        /// Método para retorno de notas, por usuario logado
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Sucesso: Arquivo salvo</response>
        /// <response code="400">Erro: Falha ao enviar arquivo</response>
        /// <response code="401">Erro: Usuário não autenticado ou não autorizado</response>
        [HttpGet]
        [Route("RetornaNotasUsuario")]
        [Authorize]
        public IActionResult RetornaNotasByUsuario()
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var NotasFiscais = _context.NotaFiscal.Where(a => a.UsuarioId == idUsuario).Select(a => new { a.Id, a.FileName, a.DataEnvio }).ToList();

            return Ok(NotasFiscais);
        }

        /// <summary>
        /// Método para upload de notas
        /// </summary>
        /// <param name="idInstituicao"></param>
        /// <param name="formDatalist"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso: Arquivo salvo</response>
        /// <response code="400">Erro: Falha ao enviar arquivo</response>
        /// <response code="401">Erro: Usuário não autenticado ou não autorizado</response>
        [HttpPost]
        [Route("UploadNotas")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UploadArquivoAsync(int idInstituicao, List<IFormFile> formDatalist)
        {
            if (formDatalist.Count() == 0)
                return NotFound(new { message = "Faça o upload do arquivo" });

            if (_context.Instituicoes.Where(a => a.Id == idInstituicao).Count() == 0 || idInstituicao == 0)
                return NotFound(new { message = "Insituição não encontrada ou id igual a zero." });

            List<NotaFiscal> notasFiscais = new();

            foreach (var formData in formDatalist)
            {
                if (formData != null)
                {
                    byte[] BytesArquivo;
                    using (var memoryStream = new MemoryStream())
                    {
                        await formData.CopyToAsync(memoryStream);
                        BytesArquivo = memoryStream.ToArray();
                    }

                    NotaFiscal nota = new()
                    {
                        ContentType = formData.ContentType,
                        FileName = formData.FileName,
                        ArrayBytes = BytesArquivo,
                        InstituicaoId = idInstituicao,
                        DataEnvio = DateTime.Now,
                        UsuarioId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value)
                    };

                    _context.NotaFiscal.Add(nota);
                    _context.SaveChanges();

                    notasFiscais.Add(nota);
                }

            }

            return new { notas = notasFiscais.Select(a => new { a.Id, a.FileName }).ToList() };
        }
    }
}
