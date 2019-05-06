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


        // GET: Reservas
        [HttpGet]
        [Authorize(Roles = "Gestor,Administrador")]
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

        
        public ActionResult Payment(ReservaSess reservaSession)
        {
            Reserva dataReservation = reservaSession[0];
            AspNetUser user = db.AspNetUsers.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
            Habitacion habitacion = db.Habitaciones.Find(dataReservation.Habitacion.Id);

            if (Request.Form["card_number"] == null) return View(reservaSession);
            Reserva newReservation = new Reserva(user, habitacion, dataReservation.check_in, dataReservation.check_out);
            db.Reservas.Add(newReservation);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Reservas/ViewReserva
        [Authorize(Roles = "Cliente")]
        public ActionResult ViewReserva()
        {
            AspNetUser user = db.AspNetUsers.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
            // Reserva reserva = db.Reservas.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            
            
           
            return View(db.Reservas.Where(res => res.AspNetUser.Id == user.Id).ToList());
            
        }

    }
}
