using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiAutomation.Models;
using Newtonsoft.Json;


namespace TaxiAutomation.Controllers
{
    public class DangerController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage DangerDetails(DangerViewModel e)
        {
            if (ModelState.IsValid)
            {
               Danger d= new Danger();

                d.CustomerId = e.CustomerId;
                d.TransportId = e.TransportId;
                d.DangerLocationLatitude=e.DangerLocationLatitude;
                d.DangerLocationLongitude=e.DangerLocationLongitude;

                new Danger().CreateDanger(d);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
