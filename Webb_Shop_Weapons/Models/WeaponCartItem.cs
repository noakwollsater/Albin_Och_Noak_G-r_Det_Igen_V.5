namespace Webb_Shop_Weapons.Models
{
    public class WeaponCartItem : CartItem
    {
        public int WeaponId { get; set; }
        public Weapon Weapon { get; set; }

    }
}
