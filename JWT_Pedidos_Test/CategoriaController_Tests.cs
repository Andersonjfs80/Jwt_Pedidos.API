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
    public class CategoriaController_Tests
    {
        CategoriaController _controller;
        ICategoriaRepository _categoriaRepository;
        ICategoriaService _categoriaService;
        private ApplicationContext _applicationContext;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _categoriaRepository = new CategoriaRepository(_applicationContext);
            _categoriaService = new CategoriaService(_categoriaRepository);
            _controller = new CategoriaController(_categoriaService);
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
                var id = _categoriaService.GetAllAsync().Result.LastOrDefault().CategoriaId;
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
                var categoria = new Categoria() { Nome = "Bebidas", Status = true };
                // Act
                var okResult = _controller.Create(categoria);
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
                var _updatingCategoria = _categoriaService.GetAllAsync().Result.LastOrDefault();

                _updatingCategoria.Nome = string.Concat(_updatingCategoria.Nome, " Teste");
                // Act
                var okResult = _controller.Update(_updatingCategoria.CategoriaId, _updatingCategoria);
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
                var _deletingCategoria = _categoriaService.GetByIdAsync(c => c.Nome.Contains("Bebidas")).Result;
                // Act
                var okResult = _controller.Delete(_deletingCategoria.CategoriaId);
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