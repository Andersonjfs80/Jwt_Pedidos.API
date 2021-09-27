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

        [Test]
        public void GetAll()
        {
            // Act
            var okResult = _controller.GetAll();
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void GetById(int id)
        {
            // Act
            var okResult = _controller.Get(id);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public void Create()
        {
            var categoria = new Categoria() { Nome = "Bebidas", Status = true};
            // Act
            var okResult = _controller.Create(categoria);
            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(okResult);
        }

        [Test]
        public void Update()
        {
            var categoria = new Categoria() { CategoriaId = 1, Nome = "Legumes", Status = true };
            // Act
            var okResult = _controller.Update(1, categoria);
            // Assert
            Assert.IsInstanceOf<NoContentResult>(okResult);
        }

        [Test]
        public void Delete()
        {            
            // Act
            var okResult = _controller.Delete(1);
            // Assert
            //Assert.IsInstanceOf<NoContentResult>(okResult);
            Assert.IsInstanceOf<NotFoundResult>(okResult);
        }


    }
}