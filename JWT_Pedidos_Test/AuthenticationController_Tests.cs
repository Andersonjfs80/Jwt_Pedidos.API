using Domain.Entidades;
using Infrastructure.DBConfiguration;
using Jwt_Pedidos_v1.API.Controllers;
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
        private IConfiguration _config;
        private AuthenticationController _controller;

        [SetUp]
        public void Setup()
        {
            _config = DatabaseConnection.ConnectionConfiguration;

            _controller = new AuthenticationController(_config);
        }

        [Test, Order(1)]
        public void ValidLogin()
        {
            // Act
            var okResult = _controller.Login(new Usuario() { Nome = "Anderson", Senha = "NS123456" });
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test, Order(2)]
        public void InValidLogin()
        {
            // Act
            var okResult = _controller.Login(new Usuario() { Nome = "Anderson", Senha = "SenhaErrada" });
            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(okResult);
        }
    }
}
