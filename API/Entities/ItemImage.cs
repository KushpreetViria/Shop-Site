using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{    
    public class ItemImage : Entity
    {
        //public int Id             { get; set; }
        public Item Item            { get; set; }
        public int ItemID           { get; set; }
        public string ImagePath     { get; set; }
        //public string PublicId { get; set; }
    }
}