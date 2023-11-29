using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Models
{
	public class Hospital
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Country {  get; set; }

		public ICollection<Patient> Patients { get; set; }
		public ICollection<Doctor> Doctors { get; set; }
		
	}
}
