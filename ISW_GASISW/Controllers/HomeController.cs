﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISW_GASISW.Controllers
{
    public class HomeController : Controller
    {
        ISW_GASISW.Models.gasiswEntities db = new Models.gasiswEntities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.usuario USERS)
        {
            bool ValidUser = db.usuario.Any(x => x.nombre == USERS.nombre);

            if (!ValidUser)
            {
                return RedirectToAction("Login");
            }

            string passwordBD = db.usuario.Where(x => x.nombre == USERS.nombre).Select(x => x.password).Single();

            bool PasswordMatches = false;
            if (USERS.password == passwordBD)
            {
                PasswordMatches = true;
            }
            else
            {
                PasswordMatches = false;
            }

            if (!PasswordMatches)
            {
                return RedirectToAction("Login");
            }
            else
            {
                string usuario = USERS.nombre;

                Session["Usuario"] = usuario;
                return RedirectToAction("Index");
            }
        }
    }
}