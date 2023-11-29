﻿using HealthcareSystem.Data;
using HealthcareSystem.Models;
using HealthcareSystem.ViewModel.Patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Controllers
{
	public class PatientsController : Controller
	{
		public IActionResult Index()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			IndexVM model = new IndexVM();
			
			model.Patients = context.Patients.Include(d=>d.Hospital)
				.Include(m=>m.Doctor)
				.ToList();
			
            return View(model);
		}

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

		private static int SelectedDoctorId;
		[HttpGet]
		public IActionResult Add3(int doctorId)
		{
			HealthCareDbContext context = new HealthCareDbContext();

			var doctor = context.Doctors.Where(x=>x.Id==doctorId).FirstOrDefault();
			SelectedDoctorId = doctorId;
			return View();
		}

		// Third page with confirmation about the new patient
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
