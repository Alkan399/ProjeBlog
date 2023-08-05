using Microsoft.EntityFrameworkCore;
using ProjeBlog.Models;

namespace ProjeBlog.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-7EM623P;database=MyBlog;uid=sa;pwd=123");
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
