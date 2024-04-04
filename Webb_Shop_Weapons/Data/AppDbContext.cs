using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ammo> Ammos { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WeaponCartItem> WeaponCartItems { get; set; }
        public DbSet<AmmoCartItem> AmmoCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        //vet ej om det här behövs??
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeaponCartItem>()
                .HasOne(wci => wci.Weapon)
                .WithMany(w => w.WeaponCartItems)
                .HasForeignKey(wci => wci.WeaponId);

            modelBuilder.Entity<AmmoCartItem>()
                .HasOne(aci => aci.Ammo)
                .WithMany(a => a.AmmoCartItems)
                .HasForeignKey(aci => aci.AmmoID);
        }
    }
}
