namespace API.DataTransferObj
{
	public class TransactionDetailsDTO
	{
        public string ItemName      	{ get; set; }
        public int Quantity         	{ get; set; }
		public string SellerBuyerName   { get; set; }
        public decimal UnitPrice    	{ get; set; }
	}
}