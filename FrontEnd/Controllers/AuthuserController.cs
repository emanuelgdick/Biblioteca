﻿using Frontend.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class AuthuserController : Controller
    {
        private readonly ApiUserService _apiService;

        public AuthuserController()
        {
            _apiService = new ApiUserService();
        }

        public IActionResult Login()
        {
            
            LoginRequestDTO obj = new LoginRequestDTO();
            return View(obj);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO  obj)
        {
            //var obj = new LoginRequestDTO { User = "emanuelgdick@gmail.com", Password = "manolo" };
          
            LoginResponseDTO objResponse = new LoginResponseDTO();
            objResponse = await _apiService.AuthenticateUser(obj);
            if (objResponse != null && objResponse.Token.ToString() != "")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, objResponse.Usuario.ApeyNom));

                identity.AddClaim(new Claim(ClaimTypes.Role, objResponse.Usuario.Rol));

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("APIToken", objResponse.Token);

                return Json(new { success = true,token = objResponse.Token });
               // return RedirectToAction("Index", "Home");
                

            }
            else
            {
                HttpContext.Session.SetString("APIToken", "");
                return Json(new { success = false, message = "Credenciales inválidas" });


            }
           // return View(objResponse);
            
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("APIToken", "");
            return RedirectToAction("Login", "Authuser");

        }
        public IActionResult AccessDenied()
        {

            return View();
        }
    }
}
