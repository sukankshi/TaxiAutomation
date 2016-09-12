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
    public class BookingController : ApiController
    {
        //[HttpPost]
        [HttpPost]
        public HttpResponseMessage BookTransport(BookTransport d)               //tested
        {
            if (ModelState.IsValid)
            {
                Booking b = new Booking();

                b.CustomerId = d.CustomerId;
                b.NoOfVehicles = d.NoOfVehicles;
                b.Boarding = new Location() { latitude = d.BoardingLat, longitude = d.BoardingLong };
                b.Destination = new Location() { latitude = d.DestinationLat, longitude = d.DestinationLong };


                return Request.CreateResponse(HttpStatusCode.OK, new Booking().Book(b) == 0 ? "Booked Successfully" : "Booking Failed");

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

    }
}
