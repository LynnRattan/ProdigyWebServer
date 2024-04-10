using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProdigyWeb.Models;

public partial class ProdigyDbContext : DbContext
{
    public ProdigyDbContext()
    {
    }

    public ProdigyDbContext(DbContextOptions<ProdigyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersStarredBook> UsersStarredBooks { get; set; }

    public virtual DbSet<UsersTbr> UsersTbrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ProdigyDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27F0D2825C");

            entity.HasIndex(e => e.Username, "UC_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.UserPswd).HasMaxLength(30);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<UsersStarredBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersSta__3214EC2795DA6D90");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookIsbn)
                .HasMaxLength(55)
                .HasColumnName("BookISBN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersStarredBooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersStar__UserI__72C60C4A");
        });

        modelBuilder.Entity<UsersTbr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersTBR__3214EC27075ECD1F");

            entity.ToTable("UsersTBR");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookIsbn)
                .HasMaxLength(55)
                .HasColumnName("BookISBN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersTbrs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersTBR__UserID__75A278F5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
