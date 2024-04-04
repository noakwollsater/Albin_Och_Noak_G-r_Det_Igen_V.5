namespace Webb_Shop_Weapons.Models
{
    public class Weapon
    {
        public int WeaponId { get; set; }
        public string WeaponName { get; set; }
        public string WeaponType { get; set; }
        public string AmmoType { get; set; }
        public int WeaponPrice { get; set; }
        public string WeaponDescription { get; set; }
        public string WeaponImage { get; set; }
        public int AmmoID { get; set; }
        public Ammo Ammo {  get; set; }

        public ICollection<WeaponCartItem> WeaponCartItems { get; set; }
    }
}
