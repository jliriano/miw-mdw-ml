using InstaRoomWeb.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InstaRoomWeb.Models.Binder
{
    
        public class ReservaBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext,
                ModelBindingContext bindingContext)
            {
                ReservaSess r = (controllerContext.HttpContext.Session != null) ?
                    (controllerContext.HttpContext.Session["key_cc"] as ReservaSess) :
                     null;

                if (r == null)
                {
                    r = new ReservaSess();
                    controllerContext.HttpContext.Session["key_cc"] = r;
                }

                return r;
            }
        }
    
}