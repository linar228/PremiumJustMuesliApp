using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuesliCore;
using MuesliCore.ViewModels;

namespace WebMuesli.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (DBConnect.IsLoginCorrect(model))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            else
                ModelState.AddModelError("", "Введите логин и пароль");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (DBConnect.RegisterUser(model))
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            else
                ModelState.AddModelError("", "Проверьте правильность введнных данных");
            return View(model);
        }
    }
}
