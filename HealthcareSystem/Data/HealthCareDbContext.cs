
using HealthcareSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace HealthcareSystem.Data
{
	public class HealthCareDbContext : DbContext
	{
        public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor>? Doctors { get; set; }
		public DbSet<Patient>? Patients { get; set; }
		public DbSet<Hospital>? Hospitals { get; set; }

		public HealthCareDbContext()
		{
			this.Doctors = this.Set<Doctor>();
			this.Patients = this.Set<Patient>();
			this.Hospitals = this.Set<Hospital>();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = WebApplication.CreateBuilder();
			optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		}

        
    }
}
