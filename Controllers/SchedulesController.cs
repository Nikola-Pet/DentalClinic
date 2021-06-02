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
    public class SchedulesController : Controller
    {
        private readonly DentalClinicContext _context;

        private readonly IAuthenticate _authenticate;

        public SchedulesController(DentalClinicContext context, IAuthenticate authenticate)
        {
            _context = context;
            _authenticate = authenticate;
        }

        // GET: Schedules
        public async Task<IActionResult> IndexD()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            return View(_context.Schedules.Where(x=> x.DentistId == id));
        }
        public async Task<IActionResult> Index()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            return View(_context.Schedules.Where(x => x.PatientId == id));
        }

        

        public IActionResult Create()
        {
            List<Dentist> FullName = new List<Dentist>(from x in _context.Dentists select new Dentist { FirstName = x.FirstName, LastName = x.LastName });
            string[] niz = new string[FullName.Count()];
            for (int i = 0; i < FullName.Count; i++)
            {
                niz[i] = FullName[i].FirstName + " "+ FullName[i].LastName;
            }

            SelectList fn = new SelectList(niz);
            ViewData["DentistId"] =fn;

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSchedulesDto createSchedulesDto)
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            Schedule schedule = new Schedule();
            string[] fl = createSchedulesDto.DentistFN.Split(' ');
            var den = _context.Dentists.FirstOrDefault(x => x.FirstName == fl[0] && x.LastName == fl[1] );
            schedule.DentistId = den.DentistId;
            schedule.PatientId = id;
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,PatientId,DentistId,DateTime")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ScheduleId == id);
        }

        
    }
}
