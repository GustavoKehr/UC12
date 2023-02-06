using Chapter.Interfaces;
using Chapter.Models;
using Chapter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;

namespace Chapter.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase//herança da classe ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;//variável privada criada para armazenar os dados da interface

        public LoginController(IUsuarioRepository iUsuarioRepository)//injeção de dependência: o controller depende da interface
        {
            _iUsuarioRepository = iUsuarioRepository;//armazenamento das informações da interface dentro da variável privada
        }

        /// <summary>
        /// método que controla o acesso para login
        /// </summary>
        /// <param name="login">objeto com os dados dos usuario : email e senha</param>
        /// <returns>token de acesso</returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                //variável que armazena os dados do usuãrio buscado
                Usuario usuarioBuscado = _iUsuarioRepository.Login(login.Email, login.Senha);

                //caso não encontre o usuário, retorna a mensagem
                if (usuarioBuscado == null)
                {
                    return Unauthorized(new { msg = "Email e/ou Senha inválidos" });
                }

                //caso encontre, prossegue para a criação do token

                // Define os dados que serão fornecidos no token - Payload
                var minhasClaims = new[]
                {
                // armazena na Claim o e-mail do usuário autenticado
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

                // armazena na Claim o ID do usuário autenticado
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),

                // armazena na Claim o tipo de usuário que foi autenticado
                new Claim(ClaimTypes.Role, usuarioBuscado.Tipo)
            };

                // define a chave de acesso ao token
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));

                // define as credenciais do token - Header
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                // gera o token
                var meuToken = new JwtSecurityToken(
                    issuer: "Chapter",// emissor do token
                    audience: "Chapter",// destinatário do token
                    claims: minhasClaims,// dados definidos nas claims
                    expires: DateTime.Now.AddMinutes(60),// tempo de expiração
                    signingCredentials: credenciais// credenciais do token
                    );
                // retorna o Ok com o token
                return Ok(
                    new { token = new JwtSecurityTokenHandler().WriteToken(meuToken) }
                    );
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}