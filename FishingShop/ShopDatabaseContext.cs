using System;
using System.Collections.Generic;
using FishingShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FishingShop;

public partial class ShopDatabaseContext : IdentityDbContext<ApplicationUser, Role, int>
{
    public ShopDatabaseContext()
    {
    }

    public ShopDatabaseContext(DbContextOptions<ShopDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DeliveryPoint> DeliveryPoints { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<UserOld> UsersOld { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DESKTOP-8NG6GSK\\SQLEXPRESS;\nDatabase=ShopDatabaseIdentity; Trusted_Connection=True; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		base.OnModelCreating(modelBuilder);
  //      //Users
  //      modelBuilder.Entity<ApplicationUser>(entity =>
  //          entity.ToTable(name :"ApplicationUsers"));
		////Roles
		//modelBuilder.Entity<Role>(entity =>
		//	entity.ToTable(name: "ApplicationUserRoles"));
		////Roles
		//modelBuilder.Entity<IdentityRole>(entity =>
		//	entity.ToTable(name: "ApplicationRoles"));
		////Claims
		//modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
		//{
		//	entity.ToTable("UserClaims");
		//});
		////Login
		//modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
		//{
		//	entity.ToTable("UserLogins");
  //          //in case you chagned the TKey type
  //          entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
  //      });
  //      //RoleClaim
		//modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
		//{
		//	entity.ToTable("RoleClaims");

		//});
  //      //UserToken
		//modelBuilder.Entity<IdentityUserToken<string>>(entity =>
		//{
		//	entity.ToTable("UserTokens");
  //          //in case you chagned the TKey type
  //          entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

  //      });
		modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory);

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<DeliveryPoint>(entity =>
        {
            entity.HasKey(e => e.IdPoint).HasName("PK_Shop");

            entity.ToTable("DeliveryPoint");

            entity.Property(e => e.IdPoint).HasColumnName("id_point");
            entity.Property(e => e.Adress).HasColumnName("adress");
            entity.Property(e => e.Image).HasColumnName("image");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder);

            entity.ToTable("Order");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Deliverypoint).HasColumnName("deliverypoint");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.DeliverypointNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Deliverypoint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_DeliveryPoint");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.IdOrderitem);

            entity.ToTable("OrderItem");

            entity.Property(e => e.IdOrderitem).HasColumnName("id_orderitem");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview);

            entity.ToTable("Review");

            entity.Property(e => e.IdReview).HasColumnName("id_review");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Product");
        });

        modelBuilder.Entity<UserOld>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Paydata).HasColumnName("paydata");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Surname).HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
