using Application.Interfaces.Domain;
using Application.Service.Domain;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Domain;
using Jwt_Pedidos_v1.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;


namespace JWT_Pedidos_Test
{
    public class Tests
    {
        CategoriaController _controller;
        ICategoriaRepository _categoriaRepository;
        ICategoriaService _categoriaService;

        [SetUp]
        public void Setup()
        {
            //falt o dbCOntext com a configuração do banco de dados
            _categoriaRepository = new CategoriaRepository();
            _categoriaService = new CategoriaService(_categoriaRepository);
            _controller = new CategoriaController(_categoriaService);
        }

        [Test]
        public void Test1()
        {
            //Assert.Pass();
            // Act
            var okResult = _controller.GetAll();
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }
    }
}