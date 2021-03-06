using System;
using System.Collections.Generic;

namespace API.Entities
{
    //Holds info on a checked out order
    public class Transaction : Entity
    {
        //public int Id                                   { get; set; }
        public AppUser AppUser                          { get; set; }
        public int AppUserID                            { get; set; }
        
        public decimal TotalCost                        { get; set; }
        public DateTime TransactionDate                 { get; set; }
        public bool Sold                                { get; set; }
        
        public ICollection<TransactionDetails> TransactionDetails    { get; set; }
    }
}