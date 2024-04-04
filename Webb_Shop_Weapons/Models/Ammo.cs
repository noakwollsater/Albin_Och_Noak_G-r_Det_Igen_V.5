using System.ComponentModel.DataAnnotations.Schema;

namespace Webb_Shop_Weapons.Models
{
    public class Ammo
    {
        public int AmmoID { get; set; }
        public string Name { get; set; }
        public ICollection<Weapon> Weapon { get; set; }
        public ICollection<AmmoCartItem> AmmoCartItems { get; set; }
    }
}
