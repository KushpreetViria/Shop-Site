namespace API.Errors
{
    public class DbResult
    {
        public bool Success { get; set; }
        public string Details { get; set; }
        
        public DbResult(bool success,  string details =""){
            this.Success = success;
            this.Details = details;
        }
    }
}