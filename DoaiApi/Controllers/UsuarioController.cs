using AutoMapper;
using DoaiApi.Data;
using DoaiApi.Data.DTOs;
using DoaiApi.Models;
using DoaiApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DoaiApi.Controllers
{
    [Route("Doai/[controller]")]
    public class UsuarioController : Controller
    {
        private InstituicaoContext _context;
        private IMapper _mapper;

        public UsuarioController(InstituicaoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo para Adicionar um novo usuário
        /// </summary>
        /// <param UsuarioDTO="usuarioDTO"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso: Usuario cadastrado</response>
        [HttpPost]
        [Route("NovoUsuario")]
        [AllowAnonymous]
        public IActionResult NovoUsuario([FromBody] UsuarioDTO usuarioDTO)
        {

            Usuario usuario = _mapper.Map<Usuario>(usuarioDTO);

            if (_context.Usuario.Where(c => c.Login == CryptService.EncryptString_Aes(usuario.Login)).Count() > 0)
            {
                return Ok(new { message = "Login não disponivel para cadastro" });
            }

            usuario.Nome = CryptService.EncryptString_Aes(usuario.Nome);
            usuario.Login = CryptService.EncryptString_Aes(usuario.Login);
            usuario.Senha = CryptService.GetHash(usuario.Senha);

            _context.Usuario.Add(usuario);
            _context.SaveChanges();
            return Ok(new { message = "Usuário criado com sucesso!"});
        }

        /// <summary>
        /// Metodo para autenticacao do usuario, necessario para consumir os demais metodos disponiveis.
        /// </summary>
        /// <param Usuario="usuario"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso: Usuario autenticado</response>
        [HttpPost]
        [Route("Autenticacao")]
        [AllowAnonymous]
        public ActionResult<dynamic> AutenticaUsuario([FromBody] UsuarioLogin usuarioLogin)
        {

            Usuario usuario = _context.Usuario.FirstOrDefault(c => c.Login == CryptService.EncryptString_Aes(usuarioLogin.Login) && c.Senha == CryptService.GetHash(usuarioLogin.Senha) );

            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            return new
            {
                token = TokenService.GenerateToken(usuario)
            };
        }

        /// <summary>
        /// Metodo para verificar usuario autenticado.
        /// </summary>
        [HttpGet]
        [Route("UsuarioAutenticado")]
        [Authorize]
        public string UsuarioAutenticado() => String.Format("Usuario autenticado: {0}", User.Identity.Name);
    }
}
