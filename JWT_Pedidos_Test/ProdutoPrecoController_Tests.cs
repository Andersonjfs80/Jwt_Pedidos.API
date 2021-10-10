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
    public class ProdutoPrecoController_Tests
    {
        ProdutoPrecoController _controller;
        IProdutoPrecoRepository _produtoPrecoRepository;
        IProdutoPrecoService _produtoPrecoService;
        private ApplicationContext _applicationContext;
        private Produto _produtoTeste;
        private int _IdUnidade;
        private int _IdTabela;

        [SetUp]
        public void Setup()
        {
            _applicationContext = new ApplicationContext();
            _produtoPrecoRepository = new ProdutoPrecoRepository(_applicationContext);
            _produtoPrecoService = new ProdutoPrecoService(_produtoPrecoRepository);
            _controller = new ProdutoPrecoController(_produtoPrecoService);

            
            var produtoService = new ProdutoService(new ProdutoRepository(_applicationContext));
            _produtoTeste = new Produto() { Nome = "Coca cola 1Lt teste", NomeReduzido = "Coca cola 1Lt teste", Status = true };
            var booResult = produtoService.AddAsync(_produtoTeste).Result;

            var unidadeService = new UnidadeService(new UnidadeRepository(_applicationContext));
            _IdUnidade = (int)unidadeService.GetAllAsync().Result?.LastOrDefault().UnidadeId;
            if (_IdUnidade == 0)
            {
                Unidade newUnidade = new Unidade() { Nome = "Unidade", NomeReduzido = "UN", Status = true };
                var booResultado = unidadeService.AddAsync(newUnidade);
                _IdUnidade = newUnidade.UnidadeId;
            }

            var tabelaPrecoService = new TabelaPrecoService(new TabelaPrecoRepository(_applicationContext));
            _IdTabela = (int)tabelaPrecoService.GetAllAsync().Result?.LastOrDefault().TabelaPrecoId;
            if (_IdTabela == 0)
            {
                TabelaPreco newTabelaPreco = new TabelaPreco() { Nome = "Tabela padrão", Status = true };
                var booResultado = tabelaPrecoService.AddAsync(newTabelaPreco);
                _IdTabela = newTabelaPreco.TabelaPrecoId;
            }
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
                var id = _produtoPrecoService.GetAllAsync().Result.LastOrDefault().ProdutoPrecoId;
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
                var produtoPreco = new ProdutoPreco() { PrecoCusto = 0.50, PrecoVenda = 1.00, TabelaPrecoId = _IdTabela, UnidadeId = _IdUnidade, Status = true };
                // Act
                var okResult = _controller.Create(produtoPreco);
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
                var _updatingProdutoPreco = _produtoPrecoService.GetAllAsync().Result.LastOrDefault();
                _updatingProdutoPreco.PrecoCusto = 0.51;
                _updatingProdutoPreco.PrecoVenda = 1.01;

                // Act
                var okResult = _controller.Update(_updatingProdutoPreco.ProdutoPrecoId, _updatingProdutoPreco);
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
                var _deletingProdutoPreco = _produtoPrecoService.GetAllAsync().Result.LastOrDefault();
                // Act
                var okResult = _controller.Delete(_deletingProdutoPreco.ProdutoPrecoId);
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
