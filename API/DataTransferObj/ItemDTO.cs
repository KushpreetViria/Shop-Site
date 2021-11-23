using System;

namespace API.DataTransferObj
{
	public class ItemDTO
	{
        public int Id               { get; set; }
        public int SellerId         { get; set; }
        public string ImageUrl	    { get; set; }
        public string Name          { get; set; }
        public decimal Price        { get; set; }
        public string Description   { get; set; }        
        public DateTime DateListed  { get; set; }
	}
}