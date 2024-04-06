using Webb_Shop_Weapons.Models;
using static System.Net.Mime.MediaTypeNames;

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
                    new Ammo { Name = "84mm" },
                    new Ammo { Name = "7.92x57mm"}
                    // Add more ammo types if needed
                };
            database.Ammos.AddRange(ammos);
            database.SaveChanges();

            // Seed Weapons from CSV file
            var weaponsFromCSV = new List<Weapon>
                {
                    new Weapon { Name = "MK18", CategoryId = 1, AmmoId = 1, Price = 12800, Description = "High-end assembled carbine with contracted and/or overhauled parts complete with all accessories to current standards at time of build.", Image = "MK18.webp" },
                    new Weapon { Name = "M16", CategoryId = 1, AmmoId = 1, Price = 11200, Description = "The fourth generation of the M16 series featuring a full length rail system", Image = "M16.webp" },
                    new Weapon { Name = "AKM", CategoryId = 1, AmmoId = 2, Price = 11200, Description = "A gas operated assault rifle using the 7.62x39mm Soviet intermediate cartridge. Damage HS(with helmet) 120 +.Damage CH with(Bulletproof vest) 70", Image = "AKM.webp" },
                    new Weapon { Name = "AK-47", CategoryId = 1, AmmoId = 2, Price = 11200, Description = "Damage HS (with helmet) 120+ Damage CH with (Bulletproof vest) 70", Image = "AK-47.webp" },
                    new Weapon { Name = "AK-15", CategoryId = 1, AmmoId = 2, Price = 12800, Description = "The AK-15 is essentially a version of the AK-12 it is chambered for a 7.62x39mm ammunition. This ammunition has superior penetration and stopping power comparing with the 5.45x39mm ammunition or the standard 5.56x45 mm ammunition. The 7.62x39mm ammunition is still used by the military and law enforcement forces and is widely used around the world. Some operators prefer this caliber. There were also plans to chamber the AK-15 for a standard 7.62x51mm ammunition. The new weapon is better balanced and has improved ergonomics.", Image = "AK-15.webp" },
                    new Weapon { Name = "AWM", CategoryId = 2, AmmoId = 4, Price = 16000, Description = "Bolt action sniper rifle with removable box magazine. Uses .338 caliber ammo.", Image = "AWM.webp" },
                    new Weapon { Name = "SVD", CategoryId = 2, AmmoId = 3, Price = 24000, Description = "A semi-automatic sniper / designated marksman rifle chambered in 7.62x54mmR and designed as a squad support weapon.", Image = "SVD.webp" },
                    new Weapon { Name = "M82A1", CategoryId = 2, AmmoId = 5, Price = 35200, Description = "A recoil-operated semi-automatic anti-material sniper system. Damage HS (with helmet) 120+. Damage CH with (Bulletproof vest) 120+.", Image = "M82A1.webp" },
                    new Weapon { Name = "Carbon Hunter", CategoryId = 2, AmmoId = 6, Price = 8000, Description = "Carbon Hunter is a bolt action rifle mostly used for game hunting. It's being loaded with Cal 30_06 bullets.", Image = "Carbon_Hunter.webp" },
                    new Weapon { Name = "Kar98k", CategoryId = 2, AmmoId = 13, Price = 11200, Description = "A bolt action rifle known as the primary rifle used in World War II. Damage HS (with helmet) 120+. Damage CH with (Bulletproof vest) 120+.", Image = "Kar98.webp" },
                    new Weapon { Name = "Desert Eagle", CategoryId = 3, AmmoId = 8, Price = 8000, Description = "The Deagle 50 is a semi-automatic handgun notable for chambering the largest centerfire cartridge of any magazine-fed and self-loading pistol. Uses: .50 AE ammunition.", Image = "DEagle_.357.webp" },
                    new Weapon { Name = "Block 21", CategoryId = 3, AmmoId = 9, Price = 1600, Description = "The Block 21 is a full-size service pistol with a standard law enforcement round with proven knock down power. The Block 21 is a short-recoil operated striker-fire semi-automatic pistol. Uses: 0.45 ACP ammunition.", Image = "Block_21.webp" },
                    new Weapon { Name = "HS-SF19", CategoryId = 3, AmmoId = 9, Price = 4000, Description = "SF19 offers everything you need in full size a high capacity strike fired pistol. Featuring forged slide and hammer forged barrel with Melonite protective finish. Redesign slide has deeper and more pronounced from and rear serrations for easier manipulation. Uses 9mm ammo.", Image = "HS_SF19.webp" },
                    new Weapon { Name = "M1911", CategoryId = 3, AmmoId = 8, Price = 1600, Description = "The M1911 pistol originated in the late 1890s as the result of a search for a suitable self-loading (or semi-automatic) pistol to replace the variety of revolvers then in service. It is a single-action semi-automatic and magazine-fed recoil-operated pistol chambered for the .45 ACP cartridge.", Image = "M1911.webp" },
                    new Weapon { Name = "TEC01 M9", CategoryId = 3, AmmoId = 9, Price = 1600, Description = "A short recoil semi-automatic single-action/double-action pistol. Damage HS (with helmet) 120+???. Damage CH with (Bulletproof vest) 9", Image = "TEC01_M9.webp" },
                    new Weapon { Name = "Baseball bat with nails", CategoryId = 4, Price = 660, Description = "A baseball bat is a smooth wooden or metal club used in the sport of baseball. Although this one is made to hit heads not balls but it depends on the context.", Image = "Baseball_Bat_Nails.webp" },
                    new Weapon { Name = "Dildo", CategoryId = 4, Price = 5500, Description = "Does this item really need a description?", Image = "Dildo.webp" },
                    new Weapon { Name = "Improvised metal sword", CategoryId = 4, Price = 330, Description = "A sword made out of scrap metal. Even if its improvised its still deadly.", Image = "Improvised_Metal_Sword.webp" },
                    new Weapon { Name = "Katana", CategoryId = 4, Price = 8800, Description = "Katana were one of the traditionally made Japanese swords that were used by the samurai of ancient and feudal Japan.", Image = "Katana.webp" },
                    new Weapon { Name = "Wired wooden club", CategoryId = 4, Price = 330, Description = "A wooden club reinforced with barbed wire. Young prodigy child of Motika.", Image = "Wire_Wooden_Club.webp" },
                    new Weapon { Name = "AT4 HEAT", CategoryId = 5, AmmoId = 12, Price = 15500, Description = "AT-4 is a single-shot 84mm shoulder-fired recoilless launcher. It is a modern solution for a light and disposable with a man-portable anti-armored and anti-fortifications launcher. It is effective against light armored vehicles but will be useless against any heavy armor/fortifications. High-Explosive Anti-Tank (HEAT) warhead carries a shaped charge explosive meant to penetrate armor but is less effective in a wide area.", Image = "AT4_HEAT.webp" },
                    new Weapon { Name = "RPG-7", CategoryId = 5, AmmoId = 10, Price = 15221, Description = "RPG-7 is a soviet man made portable multi shot shoulder fired rocket propelled grenade launcher. Widely produced in many variants it is the most famous rocket launcher around the world. It fires a 40mm rocket propelled warhead. Warheads come in multiple sizes and purposes making the RPG a versatile weapon that has been time tested and field proven for generations.", Image = "RPG7.webp" },
                    new Weapon { Name = "VHS BG", CategoryId = 6, AmmoId = 11, Price = 14400, Description = "The VHS-BG is double action single-shot 40×46mm grenade launcher.", Image = "VHS_BG.webp" },
                    new Weapon { Name = "M249", CategoryId = 7, AmmoId = 1, Price = 20733, Description = "The M249 is a belt and magazine-fed 5.56x45 NATO light machine gun. An attached folding bipod provides stability for continuous firing.", Image = "M249.webp" },
                    new Weapon { Name = "RPK", CategoryId = 7, AmmoId = 2, Price = 19200, Description = "The RPK is a gas-operated magazine-fed weapon with an air-cooled selective fire or shoulder-fired weapon with a bipod. It is chambered in 7.62x39mm.", Image = "RPK.webp" }
                    // Add more weapons if needed
                };
            database.Weapons.AddRange(weaponsFromCSV);
            database.SaveChanges();

            database.SaveChanges();
        }
    }
}
