using Microsoft.EntityFrameworkCore;

namespace AkademiPlusSignalR2.DAL.Context
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5SF4DGP\\SQLEXPRESS; initial catalog=DbSignalR2; integrated security=true");

        }
        public DbSet<Visitor> Visitors { get; set; }
    }
}
