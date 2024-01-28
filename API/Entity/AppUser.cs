using System.ComponentModel.DataAnnotations;

namespace API.Entity
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public byte[] PasswordHash{get; set;}
        public byte[] PasswordSalt{get; set;}     
        
    }
}
