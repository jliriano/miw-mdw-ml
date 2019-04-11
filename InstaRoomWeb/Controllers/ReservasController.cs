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

namespace InstaRoomWeb.Controllers
{
    public class ReservasController : Controller
    {
        private ModelsContainer db = new ModelsContainer();

        // GET: Reservas/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(int? id, [Bind(Include = "check_in_date, check_in_hour,check_out_date, check_out_hour")] ReservaFormModel reservaFormModel)
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

            Reserva reserva = new Reserva(user, habitacion, reservaFormModel.getCheckIn(), reservaFormModel.getCheckOut()); 

            TempData["reserva"] = reserva;
            return RedirectToAction("Payment");
        }

        public ActionResult Payment()
        {
            Reserva reserva = (Reserva)TempData["reserva"];

            //TODO price details and payment

            //Despues del pago, guardar reserva:
            /*db.Reservas.Add(reserva);
            db.SaveChanges();*/

            return View();
        }

    }
}
