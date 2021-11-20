using System;
using System.Collections.Generic;

namespace API.DataTransferObj
{
	public class CartDTO
	{
        public int      Count       { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<ItemDTO> Items { get; set; }	
	}
}