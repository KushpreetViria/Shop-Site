namespace API.Entities
{
    //Holds info on a single item's transaction details
    // add seller Id here in the future
    public class OrderDetail
    {
        public int Id               { get; set; }
        public Order Order          { get; set; }
        public int OrderID          { get; set; }
        public string ItemName      { get; set; }
        public string SellerName    { get; set; }
        public int Quantity         { get; set; }
        public bool sold            { get; set; }
        public decimal UnitPrice    { get; set; }
    }
}