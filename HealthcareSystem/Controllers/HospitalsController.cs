using HealthcareSystem.Data;
using HealthcareSystem.Models;
using HealthcareSystem.ViewModel.Hospitals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Controllers
{
    public class HospitalsController : Controller
    {
		private readonly IWebHostEnvironment webHostEnvironment;

        public HospitalsController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }
        [Authorize]
        public IActionResult Index()
        {
            HealthCareDbContext context = new HealthCareDbContext();
            IndexVM model = new IndexVM();

            model.Hospitals = context.Hospitals.ToList();
            model.Doctors = context.Doctors.ToList();
            model.Patients = context.Patients.ToList();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DoctorList(int id)
        {
            HealthCareDbContext context = new HealthCareDbContext();
            IndexVM model = new IndexVM();

            model.Doctors = context.Doctors.Where(x => x.HospitalId == id).Include(d => d.Hospital).ToList();
            return View(model);
        }

        [Authorize]
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
        [Authorize]
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

            string uniqueFileName = UploededFile(model);

            HealthCareDbContext context = new HealthCareDbContext();
            Hospital hospital = new Hospital();
            hospital.Name = model.Name;
            hospital.Country = model.Country;
            hospital.HospitalPicture = uniqueFileName;

            context.Hospitals.Add(hospital);
            context.SaveChanges();

            return RedirectToAction("Index", "Hospitals");
        }

        // Method for Add Page
		private string UploededFile(AddHospitalVM model)
		{
            string uniqueFileName = null;

            if(model.HospitalImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.HospitalImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.HospitalImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
		}

        // This method is for Edit Page
		private string UploededFile2(EditVM model)
		{
			string? uniqueFileName = null;

			if (model.HospitalImage != null)
			{
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.HospitalImage.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.HospitalImage.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}

        private static Hospital? EditingHospital;
        
        [Authorize]
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

            var test = UploededFile2(model);

            model.Name = hospital.Name;
            model.Id = hospital.Id;
            model.Country = hospital.Country;
			model.HospitalPictureName = hospital.HospitalPicture;

            EditingHospital = hospital;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model) 
        {
			HealthCareDbContext context = new HealthCareDbContext();

            if(model.Name == null && model.Country == null)
            {
                return View(model);
            }

			string uniqueFileName = UploededFile2(model);

            if(uniqueFileName == null)
            {
                uniqueFileName = EditingHospital.HospitalPicture;
            }

            Hospital hospital = new Hospital();
            hospital.Id = model.Id;
            hospital.Name = model.Name;
            hospital.Country = model.Country;
            hospital.HospitalPicture = uniqueFileName;

            context.Hospitals.Update(hospital);
            context.SaveChanges();

            return RedirectToAction("Index", "Hospitals");
		}

        [Authorize]
        public IActionResult Delete(int id)
        {
			HealthCareDbContext context = new HealthCareDbContext();
            Hospital hospital = new Hospital { Id = id };
            context.Hospitals.Remove(hospital);
            context.SaveChanges();

            return RedirectToAction("Index", "Hospitals");
        }
	}
}
