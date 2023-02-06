using Chapter.Controllers;
using Chapter.Interfaces;
using Chapter.Models;
using Chapter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestChapter.Controller
{
    public class LoginControllerTest
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {   //arrange
            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel dadosLogin = new LoginViewModel();

            dadosLogin.Email = "email@eemail.com";
            dadosLogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            //act
            var resultado = controller.Login(dadosLogin);

            //assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);

        }

        [Fact]
        public void LoginController_Retornar_Token()
        {
            //arrange
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "email@email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "0";

            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);

            string issuerValidacao = "chapter.webapi";

           LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "joia";
            dadosLogin.Senha = "123";

            var controller = new LoginController(fakeRepository.Object);

            //act
            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosLogin);

            string token = resultado.Value.ToString().Split("")[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = jwtHandler.ReadJwtToken(token);

            //assert
            Assert.Equal(issuerValidacao, tokenJwt.Issuer);
        }
    }
}