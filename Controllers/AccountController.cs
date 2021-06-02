using Dental.Data;
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




        public AccountController(IAuthenticate authenticate, IMedicalRecordRepo medicalRecord)
        {
            _authenticate = authenticate;
            _medicalRecord = medicalRecord;
        }
        public IActionResult Reg()
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
        public async Task<IActionResult> LoginPatient([Bind("Email,Password")] Patient patient)
        {

            var user = _authenticate.GetPatientAsync(patient.Email, patient.Password);
            if (user == null)
            {
                return null; 
            }
            
            if (ModelState.IsValid)
            {
                var token =  _authenticate.GenerateToken(user.Result.PatientId.ToString() ,user.Result.Email, "Patient");
                
                if (token == null)
                {
                    return BadRequest(new { message = "incorect" });
                }

                 HttpContext.Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddDays(1) });


                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginDentist([Bind("Email,Password")] Dentist dentist)
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

        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return  RedirectToAction("Index", "Home");
        }



    }
}
