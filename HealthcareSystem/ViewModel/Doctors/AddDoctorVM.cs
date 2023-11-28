using HealthcareSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.ViewModel.Doctors
{
    public class AddDoctorVM
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

		[Required]
		[DisplayName("Last Name")]
        public string LastName { get; set; }

		[Required]
        [Range(0, 150)]
		[DisplayName("Age")]
        public int Age { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Department")]
        public string Department { get; set; }

		[DisplayName("Hospital")]
		public int HospitalId { get; set; }

        [Required]
        public List<Hospital> Hospitals { get; set; } = new List<Hospital>();
    }
}
