namespace ProjetoExemploAPI.Model
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        public string Referencia { get; set; }

        public Produto()
        {

        }

        public Produto(int produtoId, string referencia)
        {
            ProdutoId = produtoId;
            Referencia = referencia;
        }
    }
}
