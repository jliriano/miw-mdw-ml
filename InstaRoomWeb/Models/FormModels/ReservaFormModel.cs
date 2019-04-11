using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaRoomWeb.Models.FormModels
{
    public class ReservaFormModel
    {
   
        public DateTime check_in_date { get; set; }
        public int check_in_hour { get; set; }
        public DateTime check_out_date { get; set; }
        public int check_out_hour { get; set; }
        public bool validationError { get; set;}
        public bool availabilityError { get; set; }

        internal bool isValid()
        {
            return
                (DateTime.Compare(check_in_date, check_out_date) < 0 ||
                    (DateTime.Compare(check_in_date, check_out_date) == 0 && check_in_hour < check_out_hour));
        }

        internal DateTime getCheckIn()
        {
            return this.check_in_date.AddHours(check_in_hour);
        }

        internal DateTime getCheckOut()
        {
            return this.check_out_date.AddHours(check_out_hour);
        }
    }
}