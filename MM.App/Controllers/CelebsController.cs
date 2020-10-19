using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MM.BL;
using MM.Common;

namespace MM.App.Controllers
{
    public class CelebsController : Controller
    {
        // GET: Celebs
        public ActionResult Index()
        {
            return View(); //without data
        }

    }
}