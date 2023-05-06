using TransportationAPI.Models.Dto.UserDto;

namespace TransportationAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsuniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);

    }
}
