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
        private readonly DentalClinicContext _context;
        private readonly IAuthenticate _authenticate;

        public PatientsController(DentalClinicContext context, IAuthenticate authenticate)
        {
            _context = context;
            _authenticate = authenticate;
        }

    

       
        public async Task<IActionResult> Details()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));


            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
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

           
            var patient = await _context.Patients.FindAsync(id);
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
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

 
        public async Task<IActionResult> Delete()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));


            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
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

            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
