using Foody.WebApi.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foody.WebApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            //var databaseCreator = Database.GetService<IRelationalDatabaseCreator>();
            //if (databaseCreator != null)
            //{
            //    if (!databaseCreator.Exists())
            //        databaseCreator.Create();

            //    if (!databaseCreator.HasTables())
            //        databaseCreator.CreateTables();
            //}
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; initial catalog=ApiFoodyDb; integrated security = true; ");
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

    }
}
