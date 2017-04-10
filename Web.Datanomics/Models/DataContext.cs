using System;
using System.Linq;
using System.Data.Entity;

namespace Web.Datanomics.Models
{
    public class DataContext : DbContext
    {
        public DataContext():base("name=DefaultConnection")
        {
            
        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<OurClients> OurClients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
