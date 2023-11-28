using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HealthcareSystem.Models;

namespace HealthcareSystem.ViewModel.Doctors
{
	public class EditVM
	{
		public int Id {  get; set; }

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

		[DisplayName("test")]
		public int HospitalId {  get; set; }

		[Required]
		public List<Hospital> Hospitals { get; set; } = new List<Hospital>();
	}
}
