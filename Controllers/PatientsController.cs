using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dental.Data;
using Dental.Models;

namespace Dental.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IAuthenticate _authenticate;
        private readonly IPatientRepo _patientRepo;


        public PatientsController(IAuthenticate authenticate, IPatientRepo patientRepo)
        {
            _authenticate = authenticate;
            _patientRepo = patientRepo;
        }

    

       
        public async Task<IActionResult> Details()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));


            var patient = await _patientRepo.GetPatientbyId(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        public async Task<IActionResult> Edit()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

           
            var patient = await _patientRepo.GetPatientbyId(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PatientId,FirstName,LastName,Email,Password")] Patient patient)
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     _patientRepo.UpdatePatient(patient);
                    await _patientRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_patientRepo.PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            return View(patient);
        }

 
        public async Task<IActionResult> Delete()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));


            var patient = await _patientRepo.GetPatientbyId(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            var patient = await _patientRepo.GetPatientbyId(id);
             _patientRepo.DeletePatient(patient);
            await _patientRepo.SaveChanges();
            HttpContext.Response.Cookies.Delete("token");
            return RedirectToAction(nameof(Index));
        }
    }
}
