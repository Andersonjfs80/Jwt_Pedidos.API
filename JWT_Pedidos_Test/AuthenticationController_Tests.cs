using Domain.Entidades;
using Infrastructure.DBConfiguration;
using Jwt_Pedidos_v1.API.Controllers;
using Jwt_Pedidos_v1.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Pedidos_Test
{
    public class AuthenticationController_Tests
    {
        private AuthenticationController _controller;

        [SetUp]
        public void Setup()
        {
            var config = DatabaseConnection.ConnectionConfiguration;
            var tokenConfiguration =
               config.GetSection("JwtTokenConfiguration").Get<JwtTokenConfiguration>();

            _controller = new AuthenticationController(tokenConfiguration);
        }

        [Test, Order(1)]
        public void ValidLogin()
        {
            // Act
            var okResult = _controller.Login(new UsuarioDTO() { Nome = "Anderson", Senha = "NS123456" });
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test, Order(2)]
        public void InValidLogin()
        {
            // Act
            var okResult = _controller.Login(new UsuarioDTO() { Nome = "Anderson", Senha = "SenhaErrada" });
            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(okResult);
        }
    }
}
