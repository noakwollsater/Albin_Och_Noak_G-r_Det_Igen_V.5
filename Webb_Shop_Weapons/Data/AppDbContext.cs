﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}