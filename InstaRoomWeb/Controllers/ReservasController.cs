using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InstaRoomWeb.Models;
using InstaRoomWeb.Models.FormModels;
using InstaRoomWeb.Models.Session;

namespace InstaRoomWeb.Controllers
{
    public class ReservasController : Controller
    {
        private ModelsContainer db = new ModelsContainer();
        public Reserva reserva = new Reserva();

        public ActionResult Index()
        { 
            return View(db.Reservas.ToList());
        }

        // GET: Reservas/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(int? id, [Bind(Include = "check_in_date, check_in_hour,check_out_date, check_out_hour")] ReservaFormModel reservaFormModel, ReservaSess r)
        {
            if (reservaFormModel == null) return HttpNotFound();
            reservaFormModel.validationError = !ModelState.IsValid || !reservaFormModel.isValid();
            if (reservaFormModel.validationError) return View(reservaFormModel);

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Habitacion habitacion = db.Habitaciones.Find(id);
            if (habitacion == null) return HttpNotFound();
            reservaFormModel.availabilityError = !habitacion.estaDisponible(reservaFormModel.getCheckIn(), reservaFormModel.getCheckOut());
            if (reservaFormModel.availabilityError) return View(reservaFormModel);
            
            AspNetUser user = db.AspNetUsers.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
            

            this.reserva = new Reserva(user, habitacion, reservaFormModel.getCheckIn(), reservaFormModel.getCheckOut());
            //r.Add(reserva);
          

            TempData["reserva"] = reserva;
            r.Add(reserva);


            return RedirectToAction("Payment");
        }

        
        public ActionResult Payment(ReservaSess r)
        {
            Reserva reserva = r[0];

            AspNetUser user = db.AspNetUsers.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
            Habitacion habitacion = db.Habitaciones.Find(reserva.Habitacion.Id);
            if (Request.Form["card_number"] == null) return View(r);
            Reserva rtemp = new Reserva(user, habitacion, reserva.check_in, reserva.check_out);
            db.Reservas.Add(rtemp);
            db.SaveChanges();

            return RedirectToAction("Index");
          
        }

        

    }
}
