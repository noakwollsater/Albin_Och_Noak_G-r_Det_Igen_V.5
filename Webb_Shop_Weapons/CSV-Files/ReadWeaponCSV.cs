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

                    Weapon weapon = new Weapon();
                    weapon.WeaponName = values[0];
                    weapon.WeaponType = values[1];
                    weapon.AmmoType = values[2];
                    weapon.WeaponPrice = Convert.ToInt32(values[3]);
                    weapon.WeaponDescription = values[4];
                    weapon.WeaponImage = values[5];
                    weapons.Add(weapon);
                }
            }
            return weapons;
        }
    }
}
