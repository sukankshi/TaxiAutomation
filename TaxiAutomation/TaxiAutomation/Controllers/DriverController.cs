using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaxiAutomation.Models;

namespace TaxiAutomation.Controllers
{
    public class DriverController : ApiController
    {
        //PUT: api/driver/create
        [HttpPost]
        public HttpResponseMessage Create(CreateDriverViewModel d)                      //tested
        {
            if (ModelState.IsValid)
            {
                Driver e = new Driver();
                e.Age = d.Age;
                e.FirstName = d.FirstName;
                e.LastName = d.LastName;
                e.LicenceNo = d.LicenceNo;
                e.EmailId = d.EmailId;
                e.ContactNo = d.ContactNo;
                e.Password = d.Password;
                return Request.CreateResponse(HttpStatusCode.Created, new Driver().Create(e));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //POST: api/driver/edit
        [HttpPost]
        public HttpResponseMessage Edit(EditDriverViewModel d)               //tested   
        {
            if (ModelState.IsValid)
            {
                Driver e = new Driver();
                e.DriverId = d.DriverId;
                e.FirstName = d.FirstName;
                e.LastName = d.LastName;
                e.LicenceNo = d.LicenceNo;
                return Request.CreateResponse(HttpStatusCode.OK, new Driver().Edit(e));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //DELETE: api/driver/delete
        [HttpPost]
        public HttpResponseMessage Delete(DeleteDriverViewModel d)              //tested
        {
            if (ModelState.IsValid)
            {
                Driver e = new Driver();
                e.DriverId = d.DriverId;
                return Request.CreateResponse(HttpStatusCode.OK, new Driver().Delete(e));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        

        //POST: api/driver/checkbooking                       
        /// <summary>
        /// checks the booking status of a driver....also driver recieves the
        /// notification consisting of details of the customer
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CheckBooking(CheckBooking b)
        {
            if (ModelState.IsValid)
            {
                Driver d = new Driver();

                d.DriverId = b.Id;

                string bookingDetails = new Driver().CheckBookStatus(d);
                return Request.CreateResponse(HttpStatusCode.OK, bookingDetails);

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        //POST: api/driver/Enter_Status
        /// <summary>
        /// driver enters the status as available or unavailable in the form of boolean
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Enter_Status(EnterStatus s)
        {
            if (ModelState.IsValid)
            {
                Driver d = new Driver();
                d.Available = s.Status;
                d.DriverId = s.Id;

                new Driver().EnterStatus(d);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public HttpResponseMessage EnterBookStatus(EnterStatus s)
        {
            if (ModelState.IsValid)
            {
                Driver d = new Driver();
                d.Available = s.Status;
                d.DriverId = s.Id;

                new Driver().EnterBookStatus(d);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public HttpResponseMessage IsDriverValid(TaxiAutomation.Models.LoginViewModel model)
        {
          
            int id = new Driver().IsDriverValid(model);
            if (id != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Found, new { id = id });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { id = 0 });
            }
        }

    }
}
