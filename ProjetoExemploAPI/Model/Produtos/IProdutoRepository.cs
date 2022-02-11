using System.Threading.Tasks;

namespace ProjetoExemploAPI.Model.Produtos
{
    public interface IProdutoRepository
    {
        Task<Produto> InserirOuAtualizar(Produto produto);

        Task<Produto> ObterPorId(int IdProduto);

        Task<bool> ExcluirProduto(int IdProduto);

        Task<Produto> AtualizarProduto(Produto produto);
    }
}
