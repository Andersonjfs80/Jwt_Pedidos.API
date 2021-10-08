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
    public class ProdutoController_Tests
    {
        ProdutoController _controller;
        IProdutoRepository _produtoRepository;
        IProdutoService _produtoService;
        private ApplicationContext _applicationContext;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _produtoRepository = new ProdutoRepository(_applicationContext);
            _produtoService = new ProdutoService(_produtoRepository);
            _controller = new ProdutoController(_produtoService);
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
                var id = _produtoService.GetAllAsync().Result.LastOrDefault().ProdutoId;
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
                var produto = new Produto() { Nome = "Coca cola 1Lt", NomeReduzido = "Coca cola 1Lt", Status = true };
                // Act
                var okResult = _controller.Create(produto);
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
                var _updatingProduto = _produtoService.GetAllAsync().Result.LastOrDefault();

                _updatingProduto.Nome = string.Concat(_updatingProduto.Nome, " Teste");
                // Act
                var okResult = _controller.Update(_updatingProduto.ProdutoId, _updatingProduto);
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
                var _deletingProduto = _produtoService.GetByIdAsync(c => c.Nome.Contains("Coca cola 1Lt Teste")).Result;
                // Act
                var okResult = _controller.Delete(_deletingProduto.ProdutoId);
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
