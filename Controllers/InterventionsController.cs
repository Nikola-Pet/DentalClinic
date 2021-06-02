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
        private readonly DentalClinicContext _context;
        private readonly IAuthenticate _authenticate;

        public InterventionsController(DentalClinicContext context, IAuthenticate authenticate)
        {
            _context = context;
            _authenticate = authenticate;
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));


            return View(_context.Interventions.Where(x => x.DentistId == id));
        }

        public async Task<IActionResult> IndexP()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            var mr = _context.MedicalRecords.FirstOrDefault(x => x.PatientId == id);
            int mrId = mr.MedicalRecordNumber;

            return View(_context.Interventions.Where(x => x.MedicalRecordNumber == mrId));
        }

     

        // GET: Interventions/Create
        public IActionResult Create()
        {
            return View();
        }

        //public IActionResult CreateS()
        //{
        //    return View();
        //}

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInterventionDto createInterventionDto)
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            Intervention intervention = new Intervention();

            var mr = _context.MedicalRecords.FirstOrDefault(x => x.MedicalRecordNumber == createInterventionDto.MedicalRecordNumber);
            
            intervention.DentistId = id;
            intervention.MedicalRecordNumber = mr.MedicalRecordNumber;
            intervention.DateTime = createInterventionDto.DateTime;
            intervention.Description = createInterventionDto.Description;

            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
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
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            var mr = _context.MedicalRecords.FirstOrDefault(x => x.PatientId == schedule.PatientId);
            intervention.MedicalRecordNumber = mr.MedicalRecordNumber;
            intervention.DateTime = (DateTime)schedule.DateTime;

            return View(intervention);
        }

    }
}
