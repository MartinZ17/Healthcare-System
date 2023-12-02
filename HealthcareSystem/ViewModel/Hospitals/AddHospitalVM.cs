﻿using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.ViewModel.Hospitals
{
    public class AddHospitalVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name= "Hospital name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}
