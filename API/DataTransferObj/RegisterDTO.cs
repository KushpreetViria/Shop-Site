using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTransferObj
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string password { get; set; }
    }
}