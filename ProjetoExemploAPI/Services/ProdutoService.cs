﻿using ProjetoExemploAPI.Model.Produtos;
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

        public ProdutoService()
        {
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

        public async Task<bool> ExcluirProduto(int IdProduto)
        {
            try
            {
                bool produtoExcluido = await _produtoRepository.ExcluirProduto(IdProduto);

                return produtoExcluido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            try
            {
                Produto produtoAtualizado = await _produtoRepository.AtualizarProduto(produto);

                return produtoAtualizado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Construtores

    }
}
