using HealthcareSystem.Data;
using HealthcareSystem.ViewModel.Hospitals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Controllers
{
	public class HospitalsController : Controller
	{
		public IActionResult Index()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			IndexVM model = new IndexVM();

			model.Hospitals = context.Hospitals.ToList();
			model.Doctors = context.Doctors.ToList();
			model.Patients = context.Patients.ToList();

			return View(model);
		}

		[HttpGet]
		public IActionResult DoctorList(int id)
		{
            HealthCareDbContext context = new HealthCareDbContext();
			IndexVM model = new IndexVM();

            model.Doctors = context.Doctors.Where(x=>x.HospitalId == id).Include(d=>d.Hospital).ToList();
			return View(model);
		}

		[HttpGet]
		public IActionResult PatientList(int id)
		{
            HealthCareDbContext context = new HealthCareDbContext();
            IndexVM model = new IndexVM();

            model.Patients = model.Patients = context.Patients.Where(x=>x.HospitalId == id).Include(d => d.Hospital)
                .Include(m => m.Doctor)
                .ToList();
            return View(model);
        }

	}
}
