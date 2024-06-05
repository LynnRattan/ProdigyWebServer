using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProdigyServerBL.Models;

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

    public virtual DbSet<UsersCurrentRead> UsersCurrentRead { get; set; }

    public virtual DbSet<UsersDroppedBook> UsersDroppedBook { get; set; }

    public virtual DbSet<UsersStarredBook> UsersStarredBooks { get; set; }

    public virtual DbSet<UsersTBR> UsersTBR { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProdigyDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27F34F2CAA");

            entity.HasIndex(e => e.Username, "UC_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.UserPswd).HasMaxLength(30);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<UsersCurrentRead>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersCur__3214EC273740E6B2");

            entity.ToTable("UsersCurrentRead");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookIsbn)
                .HasMaxLength(55)
                .HasColumnName("BookISBN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersCurrentReads)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersCurr__UserI__2D27B809");
        });

        modelBuilder.Entity<UsersDroppedBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersDro__3214EC27D17BF4EF");

            entity.ToTable("UsersDroppedBook");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookIsbn)
                .HasMaxLength(55)
                .HasColumnName("BookISBN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersDroppedBooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersDrop__UserI__300424B4");
        });

        modelBuilder.Entity<UsersStarredBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersSta__3214EC27E9F52652");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookIsbn)
                .HasMaxLength(55)
                .HasColumnName("BookISBN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersStarredBooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersStar__UserI__276EDEB3");
        });

        modelBuilder.Entity<UsersTBR>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersTBR__3214EC276EED9C98");

            entity.ToTable("UsersTBR");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookIsbn)
                .HasMaxLength(55)
                .HasColumnName("BookISBN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersTBR)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersTBR__UserID__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
