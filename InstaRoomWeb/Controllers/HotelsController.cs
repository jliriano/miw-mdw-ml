using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InstaRoomWeb.Models;

namespace InstaRoomWeb.Controllers
{
    public class HotelsController : Controller
    {
        private ModelsContainer db = new ModelsContainer();

        // GET: Hotels
        public ActionResult Index()
        {
            return View(db.Hoteles.ToList());
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Gestor,Administrador")]
        public ActionResult Create([Bind(Include = "Id,nombre_hotel,nombre_director,direccion_postal,imagen_hotel")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hoteles.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

    }
}
