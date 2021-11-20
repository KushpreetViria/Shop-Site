using System;
using System.Collections.Generic;

namespace API.DataTransferObj
{
	public class OrderDTO
	{
		public int Id									{ get; set; }
        public decimal TotalCost                        { get; set; }
        public DateTime OrderDate                       { get; set; }
        public ICollection<OrderDetailDTO> OrderDetials    { get; set; }
	}
}