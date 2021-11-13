namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName {get; set;}

        public byte[] passHash { get; set; }

        public byte[] passSalt { get; set; }
    }
}