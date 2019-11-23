using KoleksiyoncuCom.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoleksiyoncuCom.DataAccess.Concrete.EntityFramework
{
    public class KoleksiyoncuComDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=UMUTCAN\SQLEXPRESS;Initial Catalog=KoleksiyoncuCom;Integrated Security=true");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seller> Sellers { get; set; }
    }
}