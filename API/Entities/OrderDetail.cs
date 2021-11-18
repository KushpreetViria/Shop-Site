namespace API.Entities
{
    //Holds info on a single item's transaction details
    public class OrderDetail
    {
        public int Id               { get; set; }
        public Order Order          { get; set; }
        public int OrderID          { get; set; }
        public int ItemID           { get; set; }
        public int Quantity         { get; set; }
        public decimal UnitPrice    { get; set; }
    }
}