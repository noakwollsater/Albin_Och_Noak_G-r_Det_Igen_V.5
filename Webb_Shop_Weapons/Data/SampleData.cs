using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "Brad"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Angelina"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Will"
                });
            }


            // Seed Categories
            var categories = new List<Category>
        {
            new Category { Name = "Assault Rifle" },
            new Category { Name = "Sniper Rifle" },
            new Category { Name = "Pistol" },
            new Category { Name = "Melee Weapon" },
            new Category { Name = "Rocket Launcher" },
            new Category { Name = "Grenade Launcher" },
            new Category { Name = "Machine Gun" }
            // Add more categories if needed
        };
            database.Categories.AddRange(categories);
            database.SaveChanges();

            // Seed Ammos
            var ammos = new List<Ammo>
        {
            new Ammo { Name = "5.56x45mm" },
            new Ammo { Name = "7.62x39mm" },
            new Ammo { Name = "7.62x54mmR" },
            new Ammo { Name = ".338" },
            new Ammo { Name = ".50 BMG" },
            new Ammo { Name = ".30-06" },
            new Ammo { Name = ".50 AE" },
            new Ammo { Name = ".45 ACP" },
            new Ammo { Name = "9x19mm" },
            new Ammo { Name = "40mm" },
            new Ammo { Name = "40x46 Grenade" },
            new Ammo { Name = "84mm" }
            // Add more ammo types if needed
        };
            database.Ammos.AddRange(ammos);
            database.SaveChanges();

            // Seed Weapons from CSV file
            var weaponsFromCSV = new List<Weapon>
        {
            // Add weapons from the provided CSV file
            new Weapon { Name = "MK18", CategoryId = 1, AmmoId = 1, Price = 12800, Description = "High-end assembled carbine...", ImageUrl = "MK18.webp" },
            new Weapon { Name = "M16", CategoryId = 1, AmmoId = 1, Price = 11200, Description = "The fourth generation of the M16 series...", ImageUrl = "M16.webp" },
            new Weapon { Name = ""}
            // Add more weapons from the CSV file
        };
            database.Weapons.AddRange(weaponsFromCSV);
            database.SaveChanges();

            database.SaveChanges();
        }
    }
}
