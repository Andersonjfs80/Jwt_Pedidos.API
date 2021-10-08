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
    public class ProdutoCategoriaController_Tests
    {
        ProdutoCategoriaController _controller;
        IProdutoCategoriaRepository _produtoCategoriaRepository;
        IProdutoCategoriaService _produtoCategoriaService;
        private ApplicationContext _applicationContext;
        private Produto _produtoTeste;
        private int _IdCategoria;
        private int _IdCategoriaItem;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _produtoCategoriaRepository = new ProdutoCategoriaRepository(_applicationContext);
            _produtoCategoriaService = new ProdutoCategoriaService(_produtoCategoriaRepository);
            _controller = new ProdutoCategoriaController(_produtoCategoriaService);

            
            var produtoService = new ProdutoService(new ProdutoRepository(_applicationContext));
            _produtoTeste = new Produto() { Nome = "Coca cola 1Lt teste", NomeReduzido = "Coca cola 1Lt teste", Status = true };
            var booResult = produtoService.AddAsync(_produtoTeste).Result;

            var categoriaService = new CategoriaService(new CategoriaRepository(_applicationContext));
            _IdCategoria = (int)categoriaService.GetAllAsync().Result?.LastOrDefault().CategoriaId;

            var categoriaItemService = new CategoriaItemService(new CategoriaItemRepository(_applicationContext));
            _IdCategoriaItem = (int)categoriaItemService.GetAllIncludingAsync(ci => ci.CategoriaItemId == _IdCategoria).Result?.LastOrDefault().CategoriaItemId;
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
                var id = _produtoCategoriaService.GetAllAsync().Result.LastOrDefault().ProdutoId;
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
                var produtoCategoria = new ProdutoCategoria() { ProdutoId = _produtoTeste.ProdutoId, CategoriaId = _IdCategoria, CategoriaItemId = _IdCategoria, Status = true };
                // Act
                var okResult = _controller.Create(produtoCategoria);
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
                var _updatingProdutoCategoria = _produtoCategoriaService.GetAllAsync().Result.LastOrDefault();

                _updatingProdutoCategoria.CategoriaItemId = 0;
                // Act
                var okResult = _controller.Update(_updatingProdutoCategoria.ProdutoCategoriaId, _updatingProdutoCategoria);
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
                var _deletingProdutoCategoria = _produtoCategoriaService.GetAllAsync().Result.LastOrDefault();
                // Act
                var okResult = _controller.Delete(_deletingProdutoCategoria.ProdutoCategoriaId);
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
