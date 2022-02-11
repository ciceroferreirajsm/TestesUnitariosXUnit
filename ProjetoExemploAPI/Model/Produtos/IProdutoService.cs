using System.Threading.Tasks;

namespace ProjetoExemploAPI.Model.Produtos
{
    public interface IProdutoService
    {
        Task<Produto> AdicionarProduto(Produto produto);

        Task<Produto> ObterPorId(int IdProduto);

        Task<bool> ExcluirProduto(int IdProduto);

        Task<Produto> AtualizarProduto(Produto produto);
    }
}
