using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class Context: IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<MyEvent> MyEvents { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<MyEventTag> MyEventTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Many-To-Many
            modelBuilder.Entity<MyEventTag>()
            .HasKey(t => new { t.MyEventId, t.TagId });
            
           
            
            
            modelBuilder.Entity<MyEventTag>()
                .HasOne(pt => pt.MyEvent)
                .WithMany(p => p.MyEventTags)
                .HasForeignKey(pt => pt.MyEventId);

            modelBuilder.Entity<MyEventTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.MyEvents)
                .HasForeignKey(pt => pt.TagId);



            var myevents = new[]
            {
                new MyEvent
                {
                    Id = 1,
                    Title = "Объявление 1",
                    Body = "Описание объявления 1"
                },
                new MyEvent
                {
                    Id = 2,
                    Title = "Объявление 2",
                    Body = "Описание объявления 2"
                }
            };




            var categories = new[]
           {
                new Category
                {
                    Id = 1,
                    Name = "День рождения",
                },
                new Category
                {
                    Id = 2,
                    Name = "Праздник",
                },
                 new Category
                {
                    Id = 3,
                    Name = "День свадьбы",
                },
                  new Category
                {
                    Id = 4,
                    Name = "Поездка",
                },
                   new Category
                {
                    Id = 5,
                    Name = "Поход",
                },
                    new Category
                {
                    Id = 6,
                    Name = "Ресторан",
                },
                    new Category
               {
                     Id = 7,
                        Name = "Кино",
               },
           };
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
