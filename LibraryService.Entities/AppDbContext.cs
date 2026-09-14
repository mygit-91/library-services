using System;
using LibraryService.Entities.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Books> Books { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Borrowings> Borrowings { get; set; }
        public DbSet<Fines> Fines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key for data models
            modelBuilder.Entity<Books>().HasKey(e => e.Book_Id);
            modelBuilder.Entity<Categories>().HasKey(e => e.Category_Id);
            modelBuilder.Entity<Members>().HasKey(e => e.Member_Id);
            modelBuilder.Entity<Staff>().HasKey(e => e.Staff_Id);
            modelBuilder.Entity<Borrowings>().HasKey(e => e.Borrow_Id);
            modelBuilder.Entity<Fines>().HasKey(e => e.Fine_Id);
        }
    }
}
