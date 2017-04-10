namespace Web.Datanomics.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Datanomics.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Datanomics.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Roles.AddOrUpdate(
                p => p.Name,
                new Models.Role { Name = "Administrator" },
                new Models.Role { Name = "Mod" }
                );
            context.SaveChanges();

            context.UserProfiles.AddOrUpdate(
                p => p.UserName,

                new Models.UserProfile { UserName = "Admin",
                FullName = "Administrator", RoleID = (from b in context.Roles
                                                      where b.Name == "Administrator"
                                                      select b.ID).First(),
                Password = "F+5oa7jCZkaQZwUugvwYaTPGZQaly4UTRt3GAb0dyJL8v65BYsnI/8x7GDDGOeLvZ8i900D+oyhjkVxVvesa5Q==",
                PasswordSalt = "100000.LcHq1nR7Mzt9pyloYbUSav6RL+A7r5eyVZ/AMcHpkHDq/A=="
                }
                );
            context.SaveChanges();

            var Menu = new List<Models.Menu>
            {
               new Models.Menu { Name = "Home", Title="HOME", isDropdown = false, isGroup = false, Visible = true, isParent = true},
               new Models.Menu { Name = "About", Title="ABOUT US", isDropdown = true, isGroup = false, Visible = true, isParent = true},
               new Models.Menu { Name = "Offerings", Title="OFFERINGS", isDropdown = true, isGroup = false, Visible = true, isParent = true},
               new Models.Menu { Name = "Customers", Title="CUSTOMERS", isDropdown = false, isGroup = false, Visible = true, isParent = true},

               //new Models.Menu { Name = "Industries", Title="INDUTRIES", isDropdown = false, isGroup = false, Visible = true, ParentID = 3},
               //new Models.Menu { Name = "Service & Solution", Title="SERVICES & SOLUTIONS", isDropdown = false, isGroup = false, Visible = true, ParentID = 3},
               //new Models.Menu { Name = "Datanomics Accelerators", Title="DATANOMICS ACCELERATORS", isDropdown = false, isGroup = false, Visible = true, ParentID = 3},
               //new Models.Menu { Name = "Technology", Title="TECHNOLOGY", isDropdown = false, isGroup = false, Visible = true, ParentID = 3},
            };
            Menu.ForEach(s => context.Menu.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var Categories = new List<Models.Categories>
            {
                new Models.Categories { Name = "Slider", Title = "Slider"},
                new Models.Categories { Name = "Solution", Title = "Solution Areas"},
                new Models.Categories { Name = "Carrers", Title = "Carrers"},
            };
            Categories.ForEach(s => context.Category.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();



            var Article = new List<Models.Article>
            {
                new Models.Article { Title = "Carson",CreateTime = System.DateTime.Now},
            };
            Article.ForEach(s => context.Articles.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

        }
    }
}
