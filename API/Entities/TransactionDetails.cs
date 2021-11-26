namespace API.Entities
{
    //Holds info on a single item's transaction details
    // add seller Id here in the future
    public class TransactionDetails : Entity
    {
        //public int Id                       { get; set; }
        public Transaction Transaction      { get; set; }
        public int TransactionID            { get; set; }
        public string ItemName              { get; set; }
        public string SellerBuyerName       { get; set; }
        public int Quantity                 { get; set; }
        public decimal UnitPrice            { get; set; }
    }
}