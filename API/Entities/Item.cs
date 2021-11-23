using System;
using System.Collections.Generic;

namespace API.Entities
{
    //Holds info on a sellable item
    public class Item
    {
        public int Id               { get; set; }
        public AppUser AppUser      { get; set; }
        public int AppUserID        { get; set; }
        public ItemImage ItemImage  { get; set; }
        public string Name          { get; set; }
        public decimal Price        { get; set; }
        public string Description   { get; set; }        
        public DateTime DateListed  { get; set; } = DateTime.Now;
        public ICollection<Cart> Carts { get; set; }
    }
}