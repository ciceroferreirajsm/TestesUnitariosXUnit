using Microsoft.EntityFrameworkCore;
using ProjetoExemploAPI.Model;
using System.Linq;

namespace ProjetoExemploAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options): base(options)
        { 

        }
        public DbSet<Produto> Produtos { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.ApplyConfigurationsFromAssembly(typeof(MyContext).Assembly);

        //    //foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        //    base.OnModelCreating(builder);

        //    new ProdutoConfig(builder.Entity<Produto>());
        //}

    }
}
