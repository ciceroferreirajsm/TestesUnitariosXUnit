using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjetoExemploAPI.Context;
using ProjetoExemploAPI.Model;
using System;
using System.Threading.Tasks;

namespace ProjetoExemploAPI.repositories
{
    public class ProdutoRepository : BaseRepository, IProdutoRepository
    {
        public ProdutoRepository(MyContext contexto, ILogger logger, IMapper mapper) : base(contexto, logger, mapper)
        {
        }

        public async Task<Produto> InserirOuAtualizar(Produto produto)
        {
            try
            {
                Produto obj = await _contexto.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == produto.ProdutoId);

                if (obj == null)
                {
                    await _contexto.Produtos.AddAsync(produto);

                    _contexto.SaveChanges();

                    return produto;
                }
                else
                {
                    obj = _mapper.Map(produto, obj);

                    _contexto.Produtos.Update(obj);

                    _contexto.SaveChanges();

                    return obj;
                }
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
                Produto obj = await _contexto.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == IdProduto);

                return obj;
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
                Produto obj = await _contexto.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == IdProduto);
                
                _contexto.Produtos.Remove(obj);

                _contexto.SaveChanges();

                obj = await _contexto.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == IdProduto);

                if (obj == null)
                {
                    return true;
                }

                return false;
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
                Produto obj = await _contexto.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.ProdutoId == produto.ProdutoId);

                if (obj == null)
                {
                    return obj;
                }
                else
                {
                    obj = new Produto()
                    {
                        ProdutoId = produto.ProdutoId,
                        Referencia = produto.Referencia
                    };

                    _contexto.Produtos.Update(obj);

                    _contexto.SaveChanges();

                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
