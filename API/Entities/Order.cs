using System;
using System.Collections.Generic;

namespace API.Entities
{
    //Holds info on a checked out order
    public class Order
    {
        public int Id                                   { get; set; }
        public AppUser AppUser                          { get; set; }
        public int AppUserID                            { get; set; }
        
        public decimal TotalCost                        { get; set; }
        public DateTime OrderDate                       { get; set; }
        public ICollection<OrderDetail> OrderDetials    { get; set; }
    }
}