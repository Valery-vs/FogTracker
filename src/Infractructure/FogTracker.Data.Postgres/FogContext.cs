namespace FogTracker.Data.Postgres
{
    using Contracts.Services;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;

    public class FogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=FogTracker;User Id=postgres;Password=postgres; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = -1,
                    Username = "Admin",
                    FirstName = "Administrator",
                    PasswordHash = "9bOxITQiLXcN0l++fRR9+hGYS131VZj9puE6ICqv9Sc="
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}