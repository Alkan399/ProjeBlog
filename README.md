# ProjeBlog
using Microsoft.EntityFrameworkCore;
using ProjeBlog.Models;
using System;

namespace ProjeBlog.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-S40PS1C;database=MyBlog;uid=sa;pwd=123;Trusted_Connection=True", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppUserDetail> UsersDetails { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Basvuru> Basvuru { get; set; }
    }
}
