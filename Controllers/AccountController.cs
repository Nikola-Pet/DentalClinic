using Dental.Data;
using Dental.DTOs;
using Dental.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dental.Controllers
{

    public class AccountController : Controller
    {

        private readonly IAuthenticate _authenticate;
        private readonly IMedicalRecordRepo _medicalRecord;
        private readonly IPatientRepo _patientRepo;





        public AccountController(IAuthenticate authenticate, IMedicalRecordRepo medicalRecord, IPatientRepo patientRepo)
        {
            _authenticate = authenticate;
            _medicalRecord = medicalRecord;
            _patientRepo = patientRepo;
        }
        public IActionResult Reg()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Log()
        {
            
            return View();
        }

        public IActionResult LogD()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration([Bind("PatientId,FirstName,LastName,Email,Password")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Password = Data.Utility.Encrypt(patient.Password);
                await _authenticate.AddPatientAsync(patient);
                await _medicalRecord.AddMedicalRecordAsync(patient.PatientId);
                return RedirectToAction("Log", "Account");
            }


            return View();
        }

        [HttpPost]
        public IActionResult LoginPatient([Bind("Email,Password")] Patient patient)
        {
            try
            {
                var user = _authenticate.GetPatientAsync(patient.Email, patient.Password);
                if (user == null)
                {
                    return null;
                }

                if (ModelState.IsValid)
                {
                    var token = _authenticate.GenerateToken(user.Result.PatientId.ToString(), user.Result.Email, "Patient");

                   
                    HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddDays(1) });


                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "incorect, go back and try again" });
            }
        }

        [HttpPost]
        public  IActionResult LoginDentist([Bind("Email,Password")] Dentist dentist)
        {

            var user = _authenticate.GetDentistAsync(dentist.Email, dentist.Password);
            if (user == null)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                var token = _authenticate.GenerateToken(user.Result.DentistId.ToString(), user.Result.Email, "Dentist");

                if (token == null)
                {
                    return BadRequest(new { message = "incorect" });
                }

                HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddDays(1) });

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return  RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            string token;
            HttpContext.Request.Cookies.TryGetValue("token", out token);
            int id = int.Parse(_authenticate.ValidateIdJwtToken(token));


            var patient = await _patientRepo.GetPatientbyId(id);
            if (patient == null)
            {
                return NotFound();
            }
            if (patient.Password != Data.Utility.Encrypt(changePasswordDto.Password))
            {
                return BadRequest(new { message = "The password is incorrect" });
            }
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return BadRequest(new { message = "The new password is incorrect" });
            }

            patient.Password = Data.Utility.Encrypt(changePasswordDto.NewPassword);
            _patientRepo.UpdatePatient(patient);
            await _patientRepo.SaveChanges();

            return  RedirectToAction("Details", "Patients");
        }




    }
}
