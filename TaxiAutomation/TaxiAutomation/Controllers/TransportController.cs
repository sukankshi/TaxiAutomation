using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiAutomation.Models;

namespace TaxiAutomation.Controllers
{
    public class TransportController : ApiController
    {
        //POST: api/transport/create
        [HttpPost]
        public HttpResponseMessage Create(CreateTransportViewModel t)                       //tested
        {
            if (ModelState.IsValid)
            {
                Transport e = new Transport();
                e.RegistrationNo = t.RegistrationNo;
                e.VehicleNo = t.VehicleNo;
                e.TransportDriver.DriverId = t.DriverId;
                e.TransportType = t.TransportType == 0 ? TransportEnum.Taxi : TransportEnum.Autorickshaw;
                e.Location = new Location() { latitude = t.LocationLatitude, longitude = t.LocationLongitude };
                //e.Available = t.Available;
                //e.Booked = t.Booked;
                return Request.CreateResponse(HttpStatusCode.Created, new Transport().Create(e));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //PUT: api/transport/edit
        [HttpPost]
        public HttpResponseMessage Edit(EditTransportViewModel t)                           //tested
        {
            if (ModelState.IsValid)
            {
                Transport e = new Transport();
                e.TransportDriver.DriverId = t.DriverId;
                e.TransportId = t.TransportId;
                return Request.CreateResponse(HttpStatusCode.OK, new Transport().Edit(e));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //DELETE: api/transport/delete
        [HttpPost]
        public HttpResponseMessage Delete(DeleteTransportViewModel t)                       //tested
        {
            if (ModelState.IsValid)
            {
                Transport e = new Transport();
                e.TransportId = t.TransportId;
                return Request.CreateResponse(HttpStatusCode.OK, new Transport().Delete(e));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //PUT: api/transport/updatelocation
        [HttpPost]
        public HttpResponseMessage UpdateLocation(UpdateTransportLocationViewModel t)               //tested
        {
            if (ModelState.IsValid)
            {
                Transport e = new Transport();
                e.TransportId = t.TransportId;
                e.Location = new Location() { latitude = t.CurrentLocationLatitude, longitude = t.CurrentLocationLongitude };

                return Request.CreateResponse(HttpStatusCode.OK, new Transport().UpdateLocation(e));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //    //PUT: api//Driver_Location
        //    [HttpPost]
        //    public HttpResponseMessage Driver_Location(DriverLocation l)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Driver d = new Driver();
        //            d.DriverId = l.Id;
        //            d.LocationLatitude = l.LocationLatitude;
        //            d.LoctionLongitude = l.LocationLongitude;

        //            new Driver().LocateDriver(d);
        //            return Request.CreateResponse(HttpStatusCode.OK);

        //        }
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}
    }
}
