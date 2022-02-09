using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoExemploAPI.Model;

namespace ProjetoExemploAPI.Context
{
    public class ProdutoConfig
    {
        #region Contrutores

        public ProdutoConfig(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.ProdutoId);

            builder.Property(c => c.Referencia);

        }

        #endregion Construtores
    }
}
