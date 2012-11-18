using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.HyperMedia.Linker.Interfaces;

namespace RestBucks.WebApi.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         return View();
      }
   }
}
