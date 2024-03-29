﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
       : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id  =1,
                        TelegramUserId = Secrets.Const.MyTelegramId
                    }
            );
        }
    }
}
