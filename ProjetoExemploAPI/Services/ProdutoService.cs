using Microsoft.Extensions.Logging;
using ProjetoExemploAPI.Model;
using System;
using System.Threading.Tasks;

namespace ProjetoExemploAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        #region Propriedades

        private readonly IProdutoRepository _produtoRepository;

        #endregion Propriedades

        #region Construtores

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public async Task<Produto> AdicionarProduto(Produto produto)
        {
            try
            {
                Produto retornoProduto = await _produtoRepository.InserirOuAtualizar(produto);

                return retornoProduto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Produto> ObterPorId(int IdProduto)
        {
            try
            {
                Produto retornoProduto = await _produtoRepository.ObterPorId(IdProduto);

                return retornoProduto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Construtores

    }
}
