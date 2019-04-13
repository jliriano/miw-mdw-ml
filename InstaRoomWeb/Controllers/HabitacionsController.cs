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
    public class HabitacionsController : Controller
    {
        private ModelsContainer db = new ModelsContainer();

        // GET: Habitacions
        public ActionResult Index()
        {
            var habitaciones = db.Habitaciones.Include(h => h.Hotel);
            return View(habitaciones.ToList());
        }

        // GET: Habitacions
        public ActionResult ListView()
        {
            var habitaciones = db.Habitaciones.Include(h => h.Hotel);
            return View(habitaciones.ToList());
        }


        // GET: Habitacions/Details/5
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

        // GET: Habitacions/Create
        [Authorize(Roles = "Gestor, Administrador")]
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.Hoteles, "Id", "nombre_hotel");
            return View();
        }

        // POST: Habitacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Habitacions/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.HotelId = new SelectList(db.Hoteles, "Id", "nombre_hotel", habitacion.HotelId);
            return View(habitacion);
        }

        // POST: Habitacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,tipo_habitacion,servicios,precio_hr,precio_dia,HotelId")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(db.Hoteles, "Id", "nombre_hotel", habitacion.HotelId);
            return View(habitacion);
        }

        // GET: Habitacions/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Habitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitacion habitacion = db.Habitaciones.Find(id);
            db.Habitaciones.Remove(habitacion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
