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

        private readonly IAuthenticate _authenticate;
        private readonly IScheduleRepo _scheduleRepo;
        private readonly IDentistRepo _dentistRepo;



        public SchedulesController(IAuthenticate authenticate, IScheduleRepo scheduleRepo, IDentistRepo dentistRepo)
        {
            _authenticate = authenticate;
            _scheduleRepo = scheduleRepo;
            _dentistRepo = dentistRepo;
        }

        public IActionResult IndexD()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            return View(_scheduleRepo.GetSchedulebyDentistId(id));
        }
        public IActionResult Index()
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));

            return View(_scheduleRepo.GetSchedulebyPatientId(id));
        }

        

        public IActionResult Create()
        {
            List<Dentist> FullName = _dentistRepo.AllDentists();
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
            var den = _dentistRepo.GetDentistbyFullName(fl[0], fl[1]);
            schedule.DentistId = den.DentistId;
            schedule.PatientId = id;
            if (ModelState.IsValid)
            {
                _scheduleRepo.CreateSchedule(schedule);
                await _scheduleRepo.SaveChanges();
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

            var schedule = await _scheduleRepo.GetSchedulebyId(id);
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
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);

            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _scheduleRepo.UpdateSchedule(schedule);
                    await _scheduleRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_scheduleRepo.ScheduleExists(schedule.ScheduleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (_authenticate.ValidateRoleJwtToken(token) == "Dentist")
                {
                    return RedirectToAction(nameof(IndexD));

                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _scheduleRepo.GetSchedulebyId(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _scheduleRepo.GetSchedulebyId(id);
            _scheduleRepo.DeleteSchedule(schedule);
            await _scheduleRepo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
