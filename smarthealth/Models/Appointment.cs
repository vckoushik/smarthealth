﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace smarthealth.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }  


    }
}
