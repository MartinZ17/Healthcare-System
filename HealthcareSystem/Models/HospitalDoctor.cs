using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models
{
	public class HospitalDoctor
	{
		[Key]
		public int Id { get; set; }	

		public int HospitalId {  get; set; }
		public virtual Hospital Hospital {  get; set; }
		public int DoctorId {  get; set; }
		public virtual Doctor Doctor { get; set; }
	}
}
