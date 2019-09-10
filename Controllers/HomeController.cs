using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using dojodachi.Models;

namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public RedirectToActionResult Method()
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("dojodachi")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<Dachi>("DachiInfo") == null)
            {
                HttpContext.Session.SetObjectAsJson("DachiInfo", new Dachi());
            }
            ViewBag.DachiInfo = HttpContext.Session.GetObjectFromJson<Dachi>("DachiInfo");
            if (ViewBag.DachiInfo.fullness < 1 || ViewBag.DachiInfo.happiness < 1)
            {
                ViewBag.DachiInfo.status = "Uh oh. Your dachi has died.";
            }
            if (ViewBag.DachiInfo.energy >= 100 && ViewBag.DachiInfo.fullness >= 100 && ViewBag.DachiInfo.happiness >= 100)
            {
                ViewBag.DachiInfo.status = "Hooray! You are a great dachi friend!";
            }
            return View();
        }
        [HttpGet("Feed")]
        public IActionResult Feed()
        {
            Dachi DachiInfo = HttpContext.Session.GetObjectFromJson<Dachi>("DachiInfo");
            if (DachiInfo.meals > 0)
            {
                System.Console.WriteLine("Dachi has meals");
                DachiInfo.feed();
            }
            else
            {
                System.Console.WriteLine("Dachi has no meals");
                DachiInfo.status = "You have no food! Your Dachi must work to earn meals.";
            }
            HttpContext.Session.SetObjectAsJson("DachiInfo", DachiInfo);
            return RedirectToAction("Index");

        }
        [HttpGet("Play")]
        public IActionResult Play()
        {
            Dachi DachiInfo = HttpContext.Session.GetObjectFromJson<Dachi>("DachiInfo");
            if (DachiInfo.energy >= 5)
            {
                DachiInfo.play();
            }
            else
            {
                DachiInfo.status = "Your dachi is tired. It must sleep to gain energy to play.";
            }
            HttpContext.Session.SetObjectAsJson("DachiInfo", DachiInfo);
            return RedirectToAction("Index");
        }
        [HttpGet("Work")]
        public IActionResult Work()
        {
            Dachi DachiInfo = HttpContext.Session.GetObjectFromJson<Dachi>("DachiInfo");
            if (DachiInfo.energy >= 5)
            {
                DachiInfo.work();
            }
            else
            {
                DachiInfo.status = "Your dachi is too tired to work. It must sleep to gain energy.";
            }
            HttpContext.Session.SetObjectAsJson("DachiInfo", DachiInfo);
            return RedirectToAction("Index");
        }
        [HttpGet("Sleep")]
        public IActionResult Sleep()
        {
            Dachi DachiInfo = HttpContext.Session.GetObjectFromJson<Dachi>("DachiInfo");
            DachiInfo.sleep();
            HttpContext.Session.SetObjectAsJson("DachiInfo", DachiInfo);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public static class SessionExtentions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
