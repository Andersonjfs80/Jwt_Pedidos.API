using Application.Interfaces.Domain;
using Application.Service.Domain;
using Domain.Entidades;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Domain;
using Jwt_Pedidos_v1.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Pedidos_Test
{
    public class CategoriaItemController_Tests
    {
        private CategoriaItemController _controller;
        private ICategoriaItemRepository _categoriaItemRepository;
        private ICategoriaItemService _categoriaItemService;
        private ICategoriaRepository _categoriaRepository;
        private ICategoriaService _categoriaService;

        private ApplicationContext _applicationContext;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _categoriaItemRepository = new CategoriaItemRepository(_applicationContext);
            _categoriaItemService = new CategoriaItemService(_categoriaItemRepository);
            _controller = new CategoriaItemController(_categoriaItemService);

            _categoriaRepository = new CategoriaRepository(_applicationContext);
            _categoriaService = new CategoriaService(_categoriaRepository);
        }

        [Test, Order(1)]
        public void GetAll()
        {
            // Act
            var okResult = _controller.GetAll();
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test, Order(3)]
        //        [TestCase(10)]
        public void GetById()
        {
            try
            {
                // Act
                var id = _categoriaItemService.GetAllAsync().Result.LastOrDefault().CategoriaItemId;
                var okResult = _controller.Get(id);
                // Assert
                Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test, Order(2)]
        public void Create()
        {
            try
            {
                int categoriaId = _categoriaService.GetByIdAsync(c => c.Nome == "Bebidas").Result.CategoriaId;
                var categoriaItem = new CategoriaItem() { CategoriaId = categoriaId, Nome = "Refrigerantes", Status = true };
                // Act
                var okResult = _controller.Create(categoriaItem);
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
                var _updatingCategoriaItem = _categoriaItemService.GetAllAsync().Result.LastOrDefault();

                _updatingCategoriaItem.Nome = string.Concat(_updatingCategoriaItem.Nome, " Teste");
                // Act
                var okResult = _controller.Update(_updatingCategoriaItem.CategoriaItemId, _updatingCategoriaItem);
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
                var _deletingCategoriaItem = _categoriaItemService.GetByIdAsync(c => c.Nome.Contains("Refrigerantes Teste")).Result;
                // Act
                var okResult = _controller.Delete(_deletingCategoriaItem.CategoriaItemId);
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
