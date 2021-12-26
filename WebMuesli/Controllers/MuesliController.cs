using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuesliCore.ViewModels;
using MuesliCore;

namespace WebMuesli.Controllers
{
    public class MuesliController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyMix()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMix(Muesli muesli)
        {
            DBConnect.AddMuesli(muesli);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddOrder(MuesliMix mix)
        {
            DBConnect.CreateOrder(mix);
            return RedirectToAction("Order");
        }

        public IActionResult RemoveOrder(int orderId)
        {
            DBConnect.RemoveOrder(orderId);
            return RedirectToAction("Order");
        }
        public IActionResult RemoveMix(int mixId)
        {
            DBConnect.RemoveMix(mixId);
            return RedirectToAction("MyMix");
        }
    }
}
