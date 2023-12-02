using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.ViewModel.Hospitals
{
    public class EditVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Hospital name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

		public IFormFile HospitalImage { get; set; }

        public string HospitalPictureName { get; set; }

	}
}
