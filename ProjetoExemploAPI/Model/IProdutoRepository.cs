using System.Threading.Tasks;

namespace ProjetoExemploAPI.Model
{
    public interface IProdutoRepository
    {
        Task<Produto> InserirOuAtualizar(Produto produto);

        Task<Produto> ObterPorId(int IdProduto);
    }
}
