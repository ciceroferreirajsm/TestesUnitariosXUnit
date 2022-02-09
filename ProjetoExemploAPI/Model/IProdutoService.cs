using System.Threading.Tasks;

namespace ProjetoExemploAPI.Model
{
    public interface IProdutoService
    {
        Task<Produto> AdicionarProduto(Produto produto);
    }
}
