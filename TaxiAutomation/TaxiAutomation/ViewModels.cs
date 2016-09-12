using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiAutomation
{
    //customercontroller
    public class RegisterCustomerViewModel
    {
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string EmergencyNo { get; set; }

        //public int Id { get; set; }
    }

    //customercontroller
    public class DeleteCustomerViewModel
    {
        [Required]
        public string EmailId { get; set; }
    }

    //customercontroller
    public class EditCustomerViewModel
    {
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string EmergencyNo { get; set; }
        [Required]
        public string EmailId { get; set; }
    }

    
    //public class BookTransport
    //{
      
    //    //[Required]
    //    public int CustomerId { get; set; }
    //    //[Required]
    //    public int NoOfVehicles { get; set; }
    //    //[Required]
    //    public double BoardingLat { get; set; }
    //    public double BoardingLong { get; set; }
    //    public double DestinationLat { get; set; }
    //    //[Required]
    //    public Double DestinationLong { get; set; }
    //}


   // dangercontroller

   
    public class DangerViewModel
    {
        //[Required]
        public double DangerLocationLatitude { get; set; }
        //[Required]
        public double DangerLocationLongitude { get; set; }
        //[Required]
        public int CustomerId { get; set; }
        //[Required]
        public int TransportId { get; set; }
        //[Required]
        //public DateTime TimeStamp { get; set; }
    }

    //customercontroller
    public class LoginViewModel
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }

    //drivercontroller
    public class DriverLocation
    { 
        //[Required]
        public double LocationLongitude { get; set; }
        //[Required]
        public double LocationLatitude { get; set; }
        //[Required]                                                                //this id is driver id
        public int Id { get; set;}
    }

    //drivercontroller
    public class EnterStatus
    {
        //[Required]
        public bool Status { get; set; }                //this id is driver id
        //[Required]
        public int Id { get; set; }
    }

    //drivercontroller
    public class CheckBooking
    {
        //[Required]
        public int Id { get; set; }                 //this is driver id
    }

    //customercontroller
    public class KnowBookStatus
    {
        //[Required]
        public int Id { get; set; }                                         //this is customer id
    }

    //drivercontroller
    public class CreateDriverViewModel
    {
        [Required]
        public string LicenceNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //[Required]
        public int? Age { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string Password { get; set; }

    }

    //drivercontroller
    public class EditDriverViewModel
    {
        //[Required]
        public int DriverId { get; set; }
        [Required]
        public string LicenceNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }

    //drivercontroller
    public class DeleteDriverViewModel
    {
        //[Required]
        public int DriverId { get; set; }

    }

    //booking controller
    public class BookTransport
    {

        //[Required]
        public int CustomerId { get; set; }
        //[Required]
        public int NoOfVehicles { get; set; }
        //[Required]
        public Double BoardingLat { get; set; }
        public Double BoardingLong { get; set; }
        public Double DestinationLat { get; set; }
        //[Required]
        public Double DestinationLong { get; set; }
    }

    //transport controller
    public class CreateTransportViewModel
    {
        [Required]
        public string RegistrationNo { get; set; }
        [Required]
        public string VehicleNo { get; set; }
        //[Required]
        public int DriverId { get; set; }
        //[Required]
        public int TransportType { get; set; }
        //[Required]
        public float LocationLatitude { get; set; }
        //[Required]
        public float LocationLongitude { get; set; }
        //[Required]
        //public bool Available { get; set; }
        //[Required]
        //public bool Booked { get; set; }

    }

    //transport controller
    public class EditTransportViewModel
    {
        //[Required]
        public int DriverId { get; set; }
        //[Required]
        public int TransportId { get; set; }

    }

    //transport controller
    public class DeleteTransportViewModel
    {
        ////[Required]
        public int TransportId { get; set; }

    }

    //transport controller
    public class UpdateTransportLocationViewModel
    {
        public int TransportId;

        public float CurrentLocationLatitude;

        public float CurrentLocationLongitude;
    }

}