using Webb_Shop_Weapons.Models;

namespace Webb_Shop_Weapons.CSV_Files
{
    public class ReadWeaponCSV
    {
        public static List<Weapon> ReadWeapons(string filepath)
        {
            List<Weapon> weapons = new List<Weapon>();
            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var weapon = new Weapon
                    {
                        Name = values[0],
                        Category = new Category { Name = values[1] },
                        Ammo = new Ammo { Name = values[2] },
                        Price = Convert.ToDecimal(values[3]),
                        Description = values[4],
                        Image = values[5],
                    };
                    weapons.Add(weapon);
                }
            }
            return weapons;
        }
    }
}
