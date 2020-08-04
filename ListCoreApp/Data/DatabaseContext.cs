using ListCoreApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(i => i.ItemList)
                .WithMany(l => l.Items)
                .HasForeignKey(i => i.ListId);

            //Many-to-many
            modelBuilder.Entity<UserList>()
                .HasKey(ul => new { ul.UserId, ul.ListId });
            modelBuilder.Entity<UserList>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserLists)
                .HasForeignKey(ul => ul.UserId);
            modelBuilder.Entity<UserList>()
                .HasOne(ul => ul.List)
                .WithMany(u => u.UserLists)
                .HasForeignKey(ul => ul.ListId);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemList> ItemLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserList> UserLists { get; set; }
    }
}
