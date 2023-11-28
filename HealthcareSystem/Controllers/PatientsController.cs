using HealthcareSystem.Data;
using HealthcareSystem.ViewModel.Patients;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Controllers
{
	public class PatientsController : Controller
	{
		public IActionResult Index()
		{
			HealthCareDbContext context = new HealthCareDbContext();
			IndexVM model = new IndexVM();
			model.Patients = context.Patients.ToList();
	
			return View(model);
		}
	}
}
