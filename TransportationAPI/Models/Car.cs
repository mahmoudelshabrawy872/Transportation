﻿using System.ComponentModel.DataAnnotations;

namespace TransportationAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Model { get; set; }
        [MaxLength(250)]
        public string Color { get; set; }
        [MaxLength(250)]
        public string MoterNumber { get; set; }
        [MaxLength(250)]
        public string FramNumber { get; set; }
        [MaxLength(250)]
        public string PlateNumber { get; set; }
        [MaxLength(250)]
        public string? Kind { get; set; }
        public DateTime RenewalDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CheckingDate { get; set; }
        [MaxLength(250)]
        public string? OwnerName { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;




    }
}
