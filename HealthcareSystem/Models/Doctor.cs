using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Models
{
	public class Doctor
	{
		[Key]
		public int Id { get; set; }
		public string FirstName {  get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; }
		public string Department { get; set; }
		public int HospitalId { get; set; }

		[ForeignKey("HospitalId")]
		public virtual Hospital Hospital { get; set; }

		public ICollection<Patient> Patients { get; set; }

	}
}
