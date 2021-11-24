using System;
using System.Collections.Generic;

namespace API.DataTransferObj
{
	public class TransactionDTO
	{
		public int Id									{ get; set; }
        public decimal TotalCost                        { get; set; }
        public DateTime TransactionDate                 { get; set; }
		public bool sold            					{ get; set; }
        
		public ICollection<TransactionDetailsDTO> OrderDetials    { get; set; }
	}
}