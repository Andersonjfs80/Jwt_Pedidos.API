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
    }
}