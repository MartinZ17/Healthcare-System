
using HealthcareSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Data
{
	public class HealthCareDbContext : DbContext
	{
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Hospital> Hospitals { get; set; }

		public HealthCareDbContext()
		{
			this.Doctors = this.Set<Doctor>();
			this.Patients = this.Set<Patient>();
			this.Hospitals = this.Set<Hospital>();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-H8V5E3D\\MSSQLSERVER02;Database=HealthcareSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true");
		}
	}
}
