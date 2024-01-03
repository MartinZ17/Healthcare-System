using HealthcareSystem.Data;
using HealthcareSystem.Models;
using HealthcareSystem.ViewModel.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Controllers
{
	[Authorize]
	public class DoctorsController : Controller
	{
        [Authorize]
        public IActionResult Index()
		{
			HealthCareDbContext context = new HealthCareDbContext();

            var doctors = context.Doctors.Include(d => d.Hospital).ToList();

            return View(doctors);
		}

        [Authorize]
        [HttpGet]
		public IActionResult Add()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			AddDoctorVM model = new AddDoctorVM();

			model.Hospitals = context.Hospitals.ToList();
			return View(model);
		}

		[HttpPost]
		public IActionResult Add(AddDoctorVM model)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			if (!ModelState.IsValid)
			{
				model.Hospitals = context.Hospitals.ToList();
				return View(model);
			}

			Doctor doctor = new Doctor();

			doctor.FirstName = model.FirstName;
			doctor.LastName = model.LastName;
			doctor.Age = model.Age;
			doctor.Gender = model.Gender;
			doctor.HospitalId = model.HospitalId;
			doctor.Department = model.Department;

			context.Doctors.Add(doctor);
			context.SaveChanges();
			return RedirectToAction("Index", "Doctors");
		}

        [Authorize]
        [HttpGet]
		public IActionResult Edit(int id)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			Doctor item = context.Doctors.Where(x => x.Id == id).FirstOrDefault();

			if (item == null)
			{
				return RedirectToAction("Index", "Doctors");
			}

			EditVM model = new EditVM();
			model.Id = item.Id;
			model.FirstName = item.FirstName;
			model.LastName = item.LastName;
			model.Age = item.Age;
			model.Gender = item.Gender;
			model.HospitalId = item.HospitalId;
			model.Hospitals = context.Hospitals.ToList();
			model.Department = item.Department;
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditVM model)
		{
			HealthCareDbContext context = new HealthCareDbContext();

			if (!ModelState.IsValid) 
			{
				model.Hospitals = context.Hospitals.ToList();
				return View(model);
			}

			Doctor doctor = new Doctor();
			doctor.Id = model.Id;
			doctor.FirstName = model.FirstName;
			doctor.LastName = model.LastName;
			doctor.Age = model.Age;
			doctor.Gender = model.Gender;
			doctor.Department = model.Department;
			doctor.HospitalId= model.HospitalId;

			context.Doctors.Update(doctor);
			context.SaveChanges();

			return RedirectToAction("Index", "Doctors");
		}

        [Authorize]
        public IActionResult Delete(int id)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			Doctor doctor = new Doctor { Id = id };
			context.Doctors.Remove(doctor);
			context.SaveChanges();
			return RedirectToAction("Index", "Doctors");
		}
	}
}
