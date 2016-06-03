using System.Data.Entity;

namespace EfEntityTest
{
    public class MyContext : DbContext
    {
        public MyContext() : base("Data Source=.;Initial Catalog=EfEntityTest;Integrated Security=SSPI")
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        
    }
}