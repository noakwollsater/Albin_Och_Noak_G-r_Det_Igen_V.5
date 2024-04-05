using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Ammo> Ammos { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        //vet ej om det här behövs??
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.ShoppingCart)
                .WithOne(b => b.Account)
                .HasForeignKey<ShoppingCart>(b => b.AccountId);

            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.Ammo)
                .WithMany()
                .HasForeignKey(w => w.AmmoId);

            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.Category)
                .WithMany() 
                .HasForeignKey(w => w.CategoryId);

            modelBuilder.Entity<Weapon>()
                .HasOne(w => w.Ammo)
                .WithMany() // Assuming Ammo doesn't have a navigation property to Weapons
                .HasForeignKey(w => w.AmmoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
