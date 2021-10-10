using Application.Interfaces.Domain;
using Application.Service.Domain;
using Domain.Entidades;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using Jwt_Pedidos_v1.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.Json;

namespace JWT_Pedidos_Test
{
    public class TabelaPrecoController_Tests
    {
        TabelaPrecoController _controller;
        ITabelaPrecoRepository _tabelaPrecoRepository;
        ITabelaPrecoService _tabelaPrecoService;
        private ApplicationContext _applicationContext;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _tabelaPrecoRepository = new TabelaPrecoRepository(_applicationContext);
            _tabelaPrecoService = new TabelaPrecoService(_tabelaPrecoRepository);
            _controller = new TabelaPrecoController(_tabelaPrecoService);
        }

        [Test, Order(1)]
        public void GetAll()
        {
            // Act
            var okResult = _controller.GetAll();
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test, Order(2)]
//        [TestCase(10)]
        public void GetById()
        {
            try
            {
                // Act
                var id = _tabelaPrecoService.GetAllAsync().Result.LastOrDefault().TabelaPrecoId;
                var okResult = _controller.Get(id);
                // Assert
                Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test, Order(3)]
        public void Create()
        {
            try
            {
                var tabelaPreco = new TabelaPreco() { Nome = "Tabela padrão", Status = true };
                // Act
                var okResult = _controller.Create(tabelaPreco);
                // Assert
                Assert.IsInstanceOf<CreatedAtActionResult>(okResult);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test, Order(4)]
        public void Update()
        {
            try
            {
                var _updatingTabelaPreco = _tabelaPrecoService.GetAllAsync().Result.LastOrDefault();

                _updatingTabelaPreco.Nome = string.Concat(_updatingTabelaPreco.Nome, " Teste");
                // Act
                var okResult = _controller.Update(_updatingTabelaPreco.TabelaPrecoId, _updatingTabelaPreco);
                // Assert
                Assert.IsInstanceOf<NoContentResult>(okResult);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test, Order(5)]
        public void DeleteContetnResult()
        {
            try
            {                
                var _deletingTabelaPreco = _tabelaPrecoService.GetByIdAsync(u => u.Nome.Contains(" Teste")).Result;
                // Act
                var okResult = _controller.Delete(_deletingTabelaPreco.TabelaPrecoId);
                // Assert
                Assert.IsInstanceOf<NoContentResult>(okResult);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test, Order(6)]
        public void DeleteNotFoundResult()
        {
            // Act
            var okResult = _controller.Delete(0);
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(okResult);
        }
    }
}