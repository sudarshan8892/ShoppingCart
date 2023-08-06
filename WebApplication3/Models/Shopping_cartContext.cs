using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication3.ModelForView;

namespace WebApplication3.Models
{
    public partial class Shopping_cartContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public Shopping_cartContext()
        {
        }

        public Shopping_cartContext(DbContextOptions<Shopping_cartContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<CartDetail> CartDetails { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Orderdetail> Orderdetails { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(d => d.Gener)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenerId)
                    .HasConstraintName("FK_Book_Genres");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_NewCartDetails_Book");

                entity.HasOne(d => d.Shopping)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.ShoppingId)
                    .HasConstraintName("FK_NewCartDetails_ShoppingCart");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('false')");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK_Order_OrderStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_Users");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orderdetails_Book");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orderdetails_Order");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('false')");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ShoppingCart_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DelatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhoneNumber).IsFixedLength();
            });
           
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
