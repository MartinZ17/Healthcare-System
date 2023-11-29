using HealthcareSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.ViewModel.Patients
{
	public class AddPatientVM
	{
		public int Id { get; set; }

		[Required]
		[DisplayName("First name")]
		public string FirstName { get; set; }

		[Required]
		[DisplayName("Last name")]
		public string LastName { get; set; }

		[Required]
		[DisplayName("Age")]
		public int Age { get; set; }

		[Required]
		[DisplayName("Gender")]
		public string Gender { get; set; }

		[Required]
		[DisplayName("Illnes")]
		public string Illnes { get; set; }

		[Required]
		[DisplayName("Hospital")]
		public int HospitalId { get; set; }

		public List<Hospital> Hospitals { get; set; } = new List<Hospital>();

		public int DoctorId {  get; set; }
		public List<Doctor> Doctors { get; set; } = new List<Doctor>();
	}
}
