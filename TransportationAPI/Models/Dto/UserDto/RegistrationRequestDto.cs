﻿namespace TransportationAPI.Models.Dto.UserDto
{
    public class RegistrationRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
