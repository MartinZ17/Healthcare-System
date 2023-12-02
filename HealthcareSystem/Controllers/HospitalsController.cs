using HealthcareSystem.Data;
using HealthcareSystem.Models;
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

            model.Doctors = context.Doctors.Where(x => x.HospitalId == id).Include(d => d.Hospital).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult PatientList(int id)
        {
            HealthCareDbContext context = new HealthCareDbContext();
            IndexVM model = new IndexVM();

            model.Patients = model.Patients = context.Patients.Where(x => x.HospitalId == id).Include(d => d.Hospital)
                .Include(m => m.Doctor)
                .ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddHospitalVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HealthCareDbContext context = new HealthCareDbContext();
            Hospital hospital = new Hospital();
            hospital.Name = model.Name;
            hospital.Country = model.Country;

            context.Hospitals.Add(hospital);
            context.SaveChanges();

            return RedirectToAction("Index", "Hospitals");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HealthCareDbContext context = new HealthCareDbContext();
            EditVM model = new EditVM();

            Hospital? hospital = context.Hospitals.Where(x => x.Id == id).FirstOrDefault();
            if (hospital == null)
            {
                return RedirectToAction("Index", "Hospitals");
			}

            model.Name = hospital.Name;
            model.Id = hospital.Id;
            model.Country = hospital.Country;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model) 
        {
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			HealthCareDbContext context = new HealthCareDbContext();
            Hospital hospital = new Hospital();
            hospital.Id = model.Id;
            hospital.Name = model.Name;
            hospital.Country = model.Country;

            context.Hospitals.Update(hospital);
            context.SaveChanges();

            return RedirectToAction("Index", "Hospitals");
		}
	}
}
