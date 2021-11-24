using System;
using System.Collections.Generic;

namespace API.DataTransferObj
{
    public class UsersDetailDTO
    {
        public int Id               { get; set; }
        public string Username      { get; set; }

        public string FullAddress   { get; set; }

        public string Address       { get; set; }
        public string City          { get; set; }
        public string State         { get; set; }
        public string Country       { get; set; }
        public string PostalCode    { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string FirstName     { get; set; }
        public string LastName      { get; set; }
        public string Email         { get; set; }

        public ICollection<TransactionDTO> Transactions     { get; set; }
        public ICollection<ItemDTO> Items                   { get; set; }
        public DateTime DateCreated                         { get; set; }
        public CartDTO Cart                                 { get; set; }
    }
}