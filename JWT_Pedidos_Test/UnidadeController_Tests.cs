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
    public class UnidadeController_Tests
    {
        UnidadeController _controller;
        IUnidadeRepository _unidadeRepository;
        IUnidadeService _unidadeService;
        private ApplicationContext _applicationContext;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _unidadeRepository = new UnidadeRepository(_applicationContext);
            _unidadeService = new UnidadeService(_unidadeRepository);
            _controller = new UnidadeController(_unidadeService);
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
                var id = _unidadeService.GetAllAsync().Result.LastOrDefault().UnidadeId;
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
                var unidade = new Unidade() { Nome = "Unidade", NomeReduzido = "UN", Status = true };
                // Act
                var okResult = _controller.Create(unidade);
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
                var _updatingUnidade = _unidadeService.GetAllAsync().Result.LastOrDefault();

                _updatingUnidade.Nome = string.Concat(_updatingUnidade.Nome, " Teste");
                // Act
                var okResult = _controller.Update(_updatingUnidade.UnidadeId, _updatingUnidade);
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
                var _deletingUnidade = _unidadeService.GetByIdAsync(u => u.Nome.Contains(" Teste")).Result;
                // Act
                var okResult = _controller.Delete(_deletingUnidade.UnidadeId);
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