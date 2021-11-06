using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Entities are stuff we can insert into the database
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