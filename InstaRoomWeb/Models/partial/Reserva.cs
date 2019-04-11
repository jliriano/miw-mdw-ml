using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaRoomWeb.Models
{

    public partial class Reserva
    {
        static int HORAS_ENTRE_RESERVAS = 2;

        public Reserva(AspNetUser user, Habitacion habitacion, DateTime check_in, DateTime check_out)
        {
            this.AspNetUser = user;
            this.Habitacion = habitacion;
            this.check_in= check_in;
            this.check_out = check_out;

            this.precio_dia = habitacion.precio_dia;
            this.precio_hr = habitacion.precio_hr;
            this.precio_total = precio_dia * days() + precio_hr * hours();
        }

        internal bool colisiona(DateTime check_in, DateTime check_out)
        {
            bool esAnterior = DateTime.Compare(this.check_out.AddHours(HORAS_ENTRE_RESERVAS), check_in)<0;
            bool esPosterior = DateTime.Compare(check_out.AddHours(HORAS_ENTRE_RESERVAS), this.check_in)<0;
            return !esAnterior && !esPosterior;
        }

        internal bool esValida()
        {
            return 
                (this.check_in.Minute==0) &&
                (this.check_out.Minute == 0) &&
                (DateTime.Compare(DateTime.Now, this.check_in) < 0) &&
                (DateTime.Compare(this.check_in, this.check_out) < 0);
        }

        internal int days()
        {
            return (int)(check_out - check_in).TotalDays;
        }

        internal int hours()
        {
            return (check_out.Hour - check_in.Hour);
        }
    }
}
