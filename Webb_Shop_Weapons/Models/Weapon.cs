namespace Webb_Shop_Weapons.Models
{
    public class Weapon : Product
    {
        public int? AmmoId { get; set; }
        public Ammo Ammo { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

