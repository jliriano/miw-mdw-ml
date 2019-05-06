using System.Data.Entity;
using System.Linq;
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

        // GET: Habitacions/Create
        [Authorize(Roles = "Gestor, Administrador")]
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.Hoteles, "Id", "nombre_hotel");
            return View();
        }

        // POST: Habitacions/Create
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

    
    }
}
