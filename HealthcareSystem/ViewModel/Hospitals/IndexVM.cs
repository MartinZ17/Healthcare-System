using HealthcareSystem.Models;

namespace HealthcareSystem.ViewModel.Hospitals
{
	public class IndexVM
	{
		public List<Doctor>? Doctors { get; set; }
		public List<Patient>? Patients { get; set; }
		public List<Hospital> Hospitals { get; set;}

		public int DoctorsCount { get; set; }
		public int PatientsCount { get; set; }
	}
}
