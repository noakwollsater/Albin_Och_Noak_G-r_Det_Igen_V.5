namespace Webb_Shop_Weapons.Models
{
    public class AmmoCartItem : CartItem
    {
        public int AmmoID { get; set; }
        public Ammo Ammo { get; set; }
    }
}
