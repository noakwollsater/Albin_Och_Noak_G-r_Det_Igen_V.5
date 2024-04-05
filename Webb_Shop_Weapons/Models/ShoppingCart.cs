namespace Webb_Shop_Weapons.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}
