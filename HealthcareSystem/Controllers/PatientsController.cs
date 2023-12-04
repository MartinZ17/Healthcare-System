using HealthcareSystem.Data;
using HealthcareSystem.Models;
using HealthcareSystem.ViewModel.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Controllers
{
	public class PatientsController : Controller
	{
        [Authorize]
        public IActionResult Index()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			IndexVM model = new IndexVM();
			
			model.Patients = context.Patients.Include(d=>d.Hospital)
				.Include(m=>m.Doctor)
				.ToList();
			
            return View(model);
		}

        [Authorize]
        public IActionResult Add()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			AddPatientVM model = new AddPatientVM();
			model.Hospitals = context.Hospitals.ToList();
			return View(model);
		}

		[HttpPost]
		public IActionResult Add(AddPatientVM model)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			if (!ModelState.IsValid)
			{
				model.Hospitals = context.Hospitals.ToList();
				return View(model);
			}
			Patient patient = new Patient();
			patient.FirstName = model.FirstName;
			patient.LastName = model.LastName;
			patient.Age = model.Age;
			patient.Gender = model.Gender;
			patient.Illnes = model.Illnes;
			patient.HospitalId = model.HospitalId;
			patient.DoctorId = 6;

			context.Patients.Add(patient);
			context.SaveChanges();

			return RedirectToAction("Add2", "Patients");
		}

        [Authorize]
        // Second page for add new patient
        [HttpGet]
		public IActionResult Add2()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			var lastPatient = context.Patients.OrderBy(x => x.Id).LastOrDefault();
			int hospitalId = lastPatient.HospitalId;
			AddPatientVM model = new AddPatientVM();
			model.Doctors = context.Doctors.Where(x => x.HospitalId == hospitalId).Include(d=>d.Hospital).ToList();
			model.FirstName = lastPatient.FirstName;
			model.LastName = lastPatient.LastName;
			model.Illnes = lastPatient.Illnes;

			return View(model);
		}

		// Third page with confirmation about the new patient

		private static int SelectedDoctorId;
        [Authorize]
        [HttpGet]
		public IActionResult Add3(int doctorId)
		{
			HealthCareDbContext context = new HealthCareDbContext();

			var doctor = context.Doctors.Where(x=>x.Id==doctorId).FirstOrDefault();
			SelectedDoctorId = doctorId;
			return View();
		}
		
		[HttpPost]
		public IActionResult Add3()
		{
			HealthCareDbContext context = new HealthCareDbContext();

			var lastPatient = context.Patients.OrderBy(x => x.Id).LastOrDefault();
			lastPatient.DoctorId = SelectedDoctorId;
			context.Patients.Update(lastPatient);
			context.SaveChanges();

			return RedirectToAction("Index", "Patients");
		}

		private static int OldHospitalId;
		private static int NewHospitalId;
		private static bool IsDiff; // If user select another hospital, in view Edit2 to hide "Stay with current doctor"

        [Authorize]
        [HttpGet]
		public IActionResult Edit(int id)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			Patient patient = context.Patients.Where(x => x.Id == id).FirstOrDefault();

			if (patient == null)
			{
				return RedirectToAction("Index", "Patients");
			}
			EditVM model = new EditVM();
			model.Id = patient.Id;
			model.FirstName = patient.FirstName;
			model.LastName = patient.LastName;
			model.Age = patient.Age;
			model.Gender = patient.Gender;
			model.Illnes = patient.Illnes;
			model.HospitalId = patient.HospitalId;
			model.Hospitals = context.Hospitals.ToList();
			model.DoctorId = patient.DoctorId;

			OldHospitalId = patient.HospitalId;

			return View(model);
		}

		private static int EditedPatient;
		[HttpPost]
		public IActionResult Edit(EditVM model)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			Patient patient = new Patient();
			if (!ModelState.IsValid)
			{
				model.Hospitals = context.Hospitals.ToList();
				return View(model);
			}

			patient.Id = model.Id;
			patient.FirstName = model.FirstName;
			patient.LastName = model.LastName;
			patient.Age = model.Age;
			patient.Gender = model.Gender;
			patient.Illnes = model.Illnes;
			patient.HospitalId = model.HospitalId;
			patient.DoctorId = model.DoctorId;

			
			EditedPatient = model.Id;
			NewHospitalId = model.HospitalId;

			context.Patients.Update(patient);
			context.SaveChanges();
			return RedirectToAction("Edit2", "Patients");
		}

        [Authorize]
        public IActionResult Edit2()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			var lastPatient = context.Patients.Where(x => x.Id == EditedPatient).FirstOrDefault();
			int hospitalId = lastPatient.HospitalId;

			if(OldHospitalId != NewHospitalId)
			{
				IsDiff = true;
				
			} else { IsDiff = false; }

			EditVM model = new EditVM();
			model.Doctors = context.Doctors.Where(x => x.HospitalId == hospitalId).Include(d => d.Hospital).ToList();
			model.FirstName = lastPatient.FirstName;
			model.LastName = lastPatient.LastName;
			model.Illnes = lastPatient.Illnes;
			model.IsDiff = IsDiff;

			return View(model);
		}

        [Authorize]
        [HttpGet]
		public IActionResult Edit3(int doctorId)
		{
			HealthCareDbContext context = new HealthCareDbContext();

			var doctor = context.Doctors.Where(x => x.Id == doctorId).FirstOrDefault();
			SelectedDoctorId = doctorId;
			return View();
		}

		[HttpPost]
		public IActionResult Edit3()
		{
			HealthCareDbContext context = new HealthCareDbContext();

			var lastPatient = context.Patients.Where(x => x.Id == EditedPatient).FirstOrDefault();
			lastPatient.DoctorId = SelectedDoctorId;
			context.Patients.Update(lastPatient);
			context.SaveChanges();

			return RedirectToAction("Index", "Patients");
		}

        [Authorize]
        public IActionResult Delete(int id)
		{
			HealthCareDbContext context = new HealthCareDbContext();
			Patient patient = new Patient{ Id = id};
			context.Patients.Remove(patient);
			context.SaveChanges();

			return RedirectToAction("Index", "Patients");
		}
	}
}
