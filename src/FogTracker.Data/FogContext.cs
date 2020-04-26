﻿namespace FogTracker.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class FogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=FogTracker;User Id=postgres;Password=postgres; ");
        }
    }
}