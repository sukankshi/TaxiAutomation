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
    public class CustomerController : ApiController
    {

        //Post: api/customer/Create_customer
        /// <summary>
        /// registers a new customer
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPost]                                                                               //tested
        public HttpResponseMessage Create_customer(RegisterCustomerViewModel d)
        {

            if (ModelState.IsValid)
            {
                Customer c = new Customer();

                c.MobileNo = d.MobileNo;
                c.FirstName = d.FirstName;
                c.LastName = d.LastName;
                c.UserName = d.UserName;
                c.EmailId = d.EmailId;
                c.Password = d.Password;
                c.EmergencyNo = d.EmergencyNo;

                return Request.CreateResponse(HttpStatusCode.OK, new Customer().CreateCustomer(c));

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        //POST: api/customer/Edit_Customer
        /// <summary>
        /// updates certain credentials of the customer
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPost]                                                                              //tested
        public HttpResponseMessage Edit_Customer(EditCustomerViewModel d)
        {

            if (ModelState.IsValid)
            {
                Customer c = new Customer();

                c.MobileNo = d.MobileNo;
                c.FirstName = d.FirstName;
                c.LastName = d.LastName;
                c.Password = d.Password;
                c.EmergencyNo = d.EmergencyNo;
                c.EmailId = d.EmailId;

                return Request.CreateResponse(HttpStatusCode.OK, new Customer().EditCustomer(c));

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        //DELETE: api/customer/Delete_Customer
        /// <summary>
        /// deletes the customer
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPost]                                                                            //tested
        public HttpResponseMessage Delete_Customer(DeleteCustomerViewModel d)
        {

            if (ModelState.IsValid)
            {
                Customer c = new Customer();
                c.EmailId = d.EmailId;
                //c.Id = d.Id;
                // new Customer().DeleteCustomer(c);
                return Request.CreateResponse(HttpStatusCode.OK, new Customer().DeleteCustomer(c));
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        //POST: api/customer/Login
        /// <summary>
        /// end point for login: either as customer or a driver
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        [HttpPost]                                                                              //tested
        public HttpResponseMessage Login(LoginViewModel l)
        {
            if (ModelState.IsValid)
            {
                Customer c = new Customer();

                c.EmailId = l.EmailId;
                c.Password = l.Password;

                string json = new Customer().LoginCustomer(c);

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //POST: api/customer/knowbookingstatus
        /// <summary>
        /// knows the booking status of customer
        /// provides with the driver details
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage KnowBookingStatus(KnowBookStatus b)
        {
            if (ModelState.IsValid)
            {
                Customer d = new Customer();

                d.Id = b.Id;

                string bookingDetails = new Customer().KnowBookStatus(d);
                return Request.CreateResponse(HttpStatusCode.OK, bookingDetails);

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);


        }

        [HttpPost]
        public HttpResponseMessage IsCustomerValid(TaxiAutomation.Models.LoginViewModel model)
        {
            int id = new Customer().IsCustomerValid(model);
            if (id != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Found, new {id=id });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { id = 0 });
            }
        }


    }

}

