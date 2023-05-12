using System.ComponentModel.DataAnnotations;

namespace TransportationAPI.Models.Dto.DriversDto
{
    public class DriverDto
    {
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
        [MaxLength(50)]
        public string? NationalId { get; set; }
        [MaxLength(50)]
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseExpireDate { get; set; }
        [MaxLength(50)]
        public string? LicenseType { get; set; }
        [MaxLength(100)]
        public string? TrafficManagement { get; set; }
    }
}
