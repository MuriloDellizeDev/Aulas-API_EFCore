using EF_Core.Domains;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Contexts
{
    public class PedidoContext : DbContext 
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<PedidoItem>  PedidoItens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-ODQTMCL\SQLEXPRESS;Initial Catalog=Pedidos;User ID=sa;Password=sa132");

            base.OnConfiguring(optionsBuilder);
        }
        




    }
}
