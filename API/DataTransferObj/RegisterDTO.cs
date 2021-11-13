using System.ComponentModel.DataAnnotations;

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