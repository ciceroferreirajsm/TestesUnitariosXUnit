using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoExemploAPI.Model;
using System;
using System.Threading.Tasks;

namespace ProjetoExemploAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;

        private readonly IProdutoService _produtoService;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoService produtoService)
        {
            _logger = logger;
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            try
            {
                Produto retorno = await _produtoService.AdicionarProduto(produto);

                if(retorno != null)
                {
                    return StatusCode(201);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int Idproduto)
        {
            try
            {
                Produto retorno = await _produtoService.ObterPorId(Idproduto);

                if (retorno != null)
                {
                    return Ok(retorno);
                }
                else
                {
                    return BadRequest("Object was Not Found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
