using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id               { get; set; }
        public string UserName      { get; set; }
        public byte[] passHash      { get; set; }
        public byte[] passSalt      { get; set; }

        public string Address       { get; set; }
        public string City          { get; set; }
        public string State         { get; set; }
        public string Country       { get; set; }
        public string PostalCode    { get; set; }

        public DateTime DateOfBirth  {get; set; }
        public string FirstName     { get; set; }
        public string LastName      { get; set; }
        public string Email         { get; set; }

        public ICollection<Transaction>  Transactions   { get; set; }
        public ICollection<Item>   Items                { get; set; }
        public DateTime DateCreated                     { get; set; } = DateTime.Now;
        public Cart Cart { get; set; }

        // not really needed
        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }

        public string GetFullAddress(){
            if(string.IsNullOrEmpty(this.Address) && string.IsNullOrEmpty(this.City) && string.IsNullOrEmpty(this.State) &&
                string.IsNullOrEmpty(this.Country) && string.IsNullOrEmpty(this.PostalCode)){
                return $"{this.Address}, {this.City}, {this.State}, {this.Country}, {this.PostalCode}";
            }else{
                return null;
            }
        }
    }
}