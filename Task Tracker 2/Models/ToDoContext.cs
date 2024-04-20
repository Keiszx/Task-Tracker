using Microsoft.EntityFrameworkCore;

namespace Task_Tracker_2.Models
{
    public class ToDoContext : DbContext  
    {
        public ToDoContext(DbContextOptions<ToDoContext>options) : base(options) { }
        public DbSet<ToDo> toDos { get; set; } = null!;
        public DbSet<Title> titles { get; set; } = null!;
        public DbSet<Status> statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>().HasData(
               
                new Title { TitleId = "school", Name= "School"},
                new Title { TitleId = "gala", Name ="Gala"},
                new Title { TitleId = "home", Name ="Home"},
                new Title { TitleId = "videogames", Name = "Video Games" }
                );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", Name = "To Do" },
                new Status { StatusId = "closed", Name = "Done" }
                );
        }

    }
}
