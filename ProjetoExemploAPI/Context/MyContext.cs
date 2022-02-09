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

    }
}
