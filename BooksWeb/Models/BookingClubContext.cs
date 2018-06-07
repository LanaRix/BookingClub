using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BooksWeb.Models
{
    public partial class BookingClubContext : IdentityDbContext<User>
    {
        public BookingClubContext(DbContextOptions<BookingClubContext> options)
    : base(options)
        { }

        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Communications> Communications { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.Property(e => e.BookAuthor).HasMaxLength(200);

                entity.Property(e => e.BookImage).HasMaxLength(500);

                entity.Property(e => e.BookInformation).HasMaxLength(1000);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Communications>(entity =>
            {
                entity.HasKey(e => e.CommunicationId);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Communications_Books");

                entity.HasOne(d => d.Selection)
                    .WithMany(p => p.Communications)
                    .HasForeignKey(d => d.SelectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Communications_Sections");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.HasKey(e => e.SectionId);

                entity.Property(e => e.SectionImage).HasMaxLength(500);

                entity.Property(e => e.SectionInformation).HasMaxLength(1000);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => e.LikeId);

                entity.Property(e => e.LikeSum);
            });
        }
    }
}
