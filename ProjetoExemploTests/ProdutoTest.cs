using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using ProjetoExemploAPI.Context;
using ProjetoExemploAPI.Controllers;
using ProjetoExemploAPI.Model;
using ProjetoExemploAPI.repositories;
using ProjetoExemploAPI.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoExemploTests
{
    public class ProdutoTest
    {
        [Fact]
        public async Task Test_adicionar_produto()
        {
            //Arrange
            var serviceProvider = Obter_configuracoes();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<ProdutoController>();
            var Imapper = serviceProvider.GetService<IMapper>();
            var context = serviceProvider.GetService<MyContext>();
 
            var produtoRepository = new ProdutoRepository(context, logger, Imapper);
            Produto produto = new Produto(0, "referencia dos testes unitarios.");

            //Act
            produto = await produtoRepository.InserirOuAtualizar(produto);

            //Assert
            Assert.NotNull(produto);
        }

        [Fact]
        public async Task Test_ObterProduto_produto()
        {
            //Arrange
            var serviceProvider = Obter_configuracoes();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<ProdutoController>();
            var Imapper = serviceProvider.GetService<IMapper>();
            var context = serviceProvider.GetService<MyContext>();

            //Act
            var produtoRepository = new ProdutoRepository(context, logger, Imapper);
            Produto produto = await produtoRepository.ObterPorId(10);

            //Assert
            Assert.NotNull(produto);
        }


        [Fact]
        public ServiceProvider Obter_configuracoes()
        {
            //Arrange
            var connection = @"Server=(localdb)\mssqllocaldb;Database=AspCore_NovoDB;Trusted_Connection=True;";

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, Produto>()
                .ForMember(x => x.ProdutoId, opt => opt.Ignore());
            });

            IMapper mapper = config.CreateMapper();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<MyContext>(options => options.UseSqlServer(connection))
                .AddTransient<IProdutoService, ProdutoService>()
                .AddTransient<IProdutoRepository, ProdutoRepository>()
                .AddDbContext<MyContext>()
                .AddSingleton(mapper)
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
