﻿namespace Webb_Shop_Weapons.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string OpenIDIssuer { get; set; }
        public string OpenIDSubject { get; set; }
        public string Name { get; set; }

        public ShoppingCart ShoppingCart { get; set; } 

    }
}
