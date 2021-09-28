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
        Categoria _categoria = null;


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
        [TestCase(0)]
        [TestCase(1)]
        public void GetById(int id)
        {
            try
            {
                // Act
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
                _categoria = new Categoria() { Nome = "Bebidas", Status = true };
                // Act
                var okResult = _controller.Create(_categoria);
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
                _categoria.Nome = "Legumes";
                // Act
                var okResult = _controller.Update(_categoria);
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
                var _deletingCategoria = _categoriaService.GetAllAsync().Result.LastOrDefault();
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