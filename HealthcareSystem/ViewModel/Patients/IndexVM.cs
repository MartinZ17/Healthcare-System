using HealthcareSystem.Models;

namespace HealthcareSystem.ViewModel.Patients
{
	public class IndexVM
	{
		public List<Patient> Patients { get; set; }

		public List<Hospital> Hospitals { get; set; }
		public List<Doctor> Doctors {  get; set; }
	}
}
