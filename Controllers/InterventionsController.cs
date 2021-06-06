using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental.Data;
using Dental.Models;
using Dental.DTOs;

namespace Dental.Controllers
{
    public class InterventionsController : Controller
    {
        private readonly IAuthenticate _authenticate;
        private readonly IInterventionRepo _interventionRepo;
        private readonly IMedicalRecordRepo _medicalRecordRepo;
        private readonly IScheduleRepo _scheduleRepo;





        public InterventionsController(IAuthenticate authenticate, IInterventionRepo interventionRepo, IMedicalRecordRepo medicalRecordRepo, IScheduleRepo scheduleRepo)
        {
            _authenticate = authenticate;
            _interventionRepo = interventionRepo;
            _medicalRecordRepo = medicalRecordRepo;
            _scheduleRepo = scheduleRepo;
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));



            return View(_interventionRepo.GetInterventionbyDentistId(id));
        }

        public async Task<IActionResult> IndexP()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            var mr = _medicalRecordRepo.GetMedicalRecordbyPatientId(id);
            int mrId = mr.MedicalRecordNumber; 

            return View(_interventionRepo.GetInterventionbyMedicalRecordId(mrId));
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInterventionDto createInterventionDto)
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            Intervention intervention = new Intervention();

            
            intervention.DentistId = id;
            intervention.MedicalRecordNumber = _medicalRecordRepo.GetMedicalRecordNumberbyId(createInterventionDto.MedicalRecordNumber);
            intervention.DateTime = createInterventionDto.DateTime;
            intervention.Description = createInterventionDto.Description;

            if (ModelState.IsValid)
            {
                _interventionRepo.AddInterverntion(intervention);
                await _interventionRepo.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(intervention);
        }

        public async Task<IActionResult> CreateS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Intervention intervention = new Intervention();
            var schedule = await _scheduleRepo.GetSchedulebyId(id);
            if (schedule == null)
            {
                return NotFound();
            }
            var mr = _medicalRecordRepo.GetMedicalRecordbyPatientId(schedule.PatientId);
            intervention.MedicalRecordNumber = mr.MedicalRecordNumber;
            intervention.DateTime = (DateTime)schedule.DateTime;

            return View(intervention);
        }

    }
}
