namespace EnglishGram.Migrations
{
    using EnglishGram.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EnglishGram.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EnglishGram.Models.ApplicationDbContext context)
        {
            string email = "yunusdemirpolatt@gm.com";
            string password = "123456";

            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }
            if (!context.Users.Any(u => u.UserName == email))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = email, Email = email };

                manager.Create(user, password);
                manager.AddToRole(user.Id, "admin");

            }
            if (!context.Photos.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    var photo = new Photo
                    {
                        PhotoUrl = "https://placeimg.com/800/800/any",
                        SubPhotoUrl = "https://placeimg.com/800/800/nature",
                        Description = "dummy text " + i,
                        SubDescription = "sub dummy text " + i,
                        CreatedAt = DateTime.Now,
                    };
                    context.Photos.Add(photo);
                }
            }
        }
    }
}
