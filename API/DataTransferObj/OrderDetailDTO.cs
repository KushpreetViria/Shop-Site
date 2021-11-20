namespace API.DataTransferObj
{
	public class OrderDetailDTO
	{
        public int ItemID           { get; set; }
        public int Quantity         { get; set; }
        public decimal UnitPrice    { get; set; }
	}
}