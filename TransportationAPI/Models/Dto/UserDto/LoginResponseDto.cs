namespace TransportationAPI.Models.Dto.UserDto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
