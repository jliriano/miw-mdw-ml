using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaRoomWeb.Models
{

    public partial class Habitacion
    {
        public TipoHabitacion Opcion { get; set; }

        internal bool estaDisponible(DateTime check_in, DateTime check_out)
        {
            foreach (Reserva reserva in this.Reservas){
                if (reserva.colisiona(check_in, check_out)) return false;
            }
            return true;
        }
    }
}