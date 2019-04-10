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
    public class HabitacionesController : Controller
    {
        private ModelsContainer db = new ModelsContainer();

        // GET: Habitaciones
        public ActionResult Index()
        {
            var habitaciones = db.Habitaciones.Include(h => h.Hotel);
            return View(habitaciones.ToList());
        }

        // GET: Habitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitaciones.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        // GET: Habitaciones/Create
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.Hoteles, "Id", "nombre_hotel");
            return View();
        }

        // POST: Habitaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor,Administrador")]
        public ActionResult Create([Bind(Include = "Id,tipo_habitacion,servicios,precio_hr,precio_dia,HotelId,Opcion")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                habitacion.tipo_habitacion = "" + habitacion.Opcion;
                db.Habitaciones.Add(habitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hoteles, "Id", "nombre_hotel", habitacion.HotelId);
            return View(habitacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
