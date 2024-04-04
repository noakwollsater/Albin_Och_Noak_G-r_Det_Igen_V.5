namespace Webb_Shop_Weapons.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }
        public ICollection<WeaponCartItem> WeaponCartItems { get; set; }
        public ICollection<AmmoCartItem> AmmoCartItems { get; set;}
    }
}
