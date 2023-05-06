using System.ComponentModel.DataAnnotations;

namespace TransportationAPI.Models.Dto.UserDto
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
