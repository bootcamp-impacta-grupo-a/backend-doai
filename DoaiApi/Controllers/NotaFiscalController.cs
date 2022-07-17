using DoaiApi.Data;
using DoaiApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// Método para upload do arquivo
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso: Arquivo salvo</response>
        /// <response code="400">Erro: Falha ao enviar arquivo</response>
        /// <response code="401">Erro: Usuário não autenticado ou não autorizado</response>
        [HttpPost]
        [Route("UploadNota")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UploadArquivoAsync(IFormFile formData)
        {
            if (formData == null)
                return NotFound(new { message = "Faça o upload do arquivo" });

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
                ArrayBytes = BytesArquivo
            };

            _context.NotaFiscal.Add(nota);
            _context.SaveChanges();

            return new { nota.Id };
        }
    }
}
