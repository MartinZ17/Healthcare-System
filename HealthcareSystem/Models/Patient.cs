using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Models
{
	public class Patient
	{
		[Key]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; }
		public string Illnes { get; set; }
		public int HospitalId {  get; set; }
		[ForeignKey("HospitalId")]
		public virtual Hospital Hospital { get; set; }

		public int DoctorId {  get; set; }
		[ForeignKey("DoctorId")]
		public virtual Doctor Doctor { get; set; }

	}
}
