﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class DojoController : Controller
    {
        // GET: Dojo
        public ActionResult Login()
        {
            return View();
        }
    }
}