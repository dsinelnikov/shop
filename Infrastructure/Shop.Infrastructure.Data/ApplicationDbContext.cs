using Microsoft.EntityFrameworkCore;
using Shop.Domain.Core;
using Shop.Domain.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<AttributeUnit> AttributeUnits { get; set; }

        public DbSet<Domain.Core.Attribute> Attributes { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var product = modelBuilder.Entity<Product>();
            product.Property(p => p.Name)
                .IsRequired();
            product
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            product.HasOne(p => p.Brand)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            product.HasOne(p => p.ManufactureCountry)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            product.HasMany(p => p.Attributes)
                .WithOne(a => a.Product)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            var productCategory = modelBuilder.Entity<ProductCategory>();
            productCategory.Property(c => c.Name)
                .IsRequired();
            productCategory.HasOne(p => p.Parent)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            var brand = modelBuilder.Entity<Brand>();
            brand.Property(b => b.Name)
                .IsRequired();
            brand.HasAlternateKey(b => b.Name);
            brand.HasOne(b => b.RegistrationCountry)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            var country = modelBuilder.Entity<Country>();
            country.Property(c => c.Name)
                .IsRequired();

            var attributeUnit = modelBuilder.Entity<AttributeUnit>();
            attributeUnit.Property(a => a.Name)
                .IsRequired();

            var attribute = modelBuilder.Entity<Domain.Core.Attribute>();
            attribute.Property(a => a.Name)
                .IsRequired();
            attribute.HasOne(a => a.Unit)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            var productCategotyAttribute = modelBuilder.Entity<ProductCategoryAttribute>();
            productCategotyAttribute.HasOne(a => a.Attribute)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            productCategotyAttribute.HasOne(a => a.ProductCategory)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            var productAttribute = modelBuilder.Entity<ProductAttribute>();
            productAttribute.HasOne(a => a.ProductAttribyte)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            productAttribute.Property(a => a.Value)
                .IsRequired();

            var person = modelBuilder.Entity<Person>();
            person.HasOne(p => p.User)
                .WithOne(u => u.Person)
                .HasForeignKey<Person>(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            person.HasMany(p => p.Orders)
                .WithOne(o => o.Person)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            person.HasData(new[] {
                new Person{Id = 1, UserId = 1, FirstName = "Admin", LastName = "Admin"},
                new Person{Id = 2, UserId = 2, FirstName = "User", LastName = "User"}
            });

            var order = modelBuilder.Entity<Order>();
            order.Property(o => o.Created)
                .HasDefaultValueSql("getdate()");
            order.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            var orderItem = modelBuilder.Entity<OrderItem>();
            orderItem.Property(oi => oi.Price)
                .IsRequired();
            orderItem.Property(oi => oi.Count)
                .IsRequired();
            orderItem.HasOne(oi => oi.Product)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Authorization
            var user = modelBuilder.Entity<User>();
            user.HasIndex(u => u.Email)
                .IsUnique();
            user.Property(u => u.Password)
                .IsRequired();
            user.HasMany(u => u.Roles)
                .WithOne();
            user.HasData(
                new User { Id = 1, Email = "admin@mail.com", Password = "admin" },
                new User { Id = 2, Email = "user@mail.com", Password = "user" });

            var role = modelBuilder.Entity<Role>();
            role.Property(r => r.Name)
                .IsRequired();
            role.HasData(
                new Role { Id = RoleType.Admin, Name = "Admin" },
                new Role { Id = RoleType.User, Name = "User" });

            var userRole = modelBuilder.Entity<UserRole>();
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
            userRole.HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            userRole.HasOne(ur => ur.Role)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            userRole.HasData(
                new UserRole { UserId = 1, RoleId = RoleType.Admin },
                new UserRole { UserId = 2, RoleId = RoleType.User });
        }
    }
}
