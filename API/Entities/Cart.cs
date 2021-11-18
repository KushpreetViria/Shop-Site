using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Cart
    {
        public int      Id          { get; set; }
        public AppUser  AppUser     { get; set; }
        public int      AppUserID   { get; set; }
        public int      Count       { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}