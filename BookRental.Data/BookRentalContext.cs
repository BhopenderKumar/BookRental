using BookRental.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.Data
{
    public class BookRentalContext : DbContext
    {
        public BookRentalContext(DbContextOptions<BookRentalContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);
                entity.Property(b => b.ISBN).IsRequired().HasMaxLength(13); // ISBNs are typically 13 characters
                entity.Property(b => b.Genre).HasMaxLength(50);
                entity.Property(b => b.AvailableCopies).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.HasOne(r => r.Book).WithMany().HasForeignKey(r => r.BookId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.User).WithMany(u => u.Rentals).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.Property(r => r.RentalDate).IsRequired();
                entity.Property(r => r.ReturnDate).IsRequired(false);
            });
        }
    }
}