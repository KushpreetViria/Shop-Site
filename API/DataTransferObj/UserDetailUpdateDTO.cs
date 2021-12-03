using System;

namespace API.DataTransferObj
{
    public class UserDetailUpdateDTO
    {
        public string Address       { get; set; }
        public string City          { get; set; }
        public string State         { get; set; }
        public string Country       { get; set; }
        public string PostalCode    { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string FirstName     { get; set; }
        public string LastName      { get; set; }
        public string Email         { get; set; }
    }
}