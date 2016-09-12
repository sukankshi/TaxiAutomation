using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiAutomation.Models.CoreFunctionality;
using System.Configuration;
using DALHelper;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using Newtonsoft.Json;
//using System.Object;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
//using System.Runtime.Serialization.Json.DataContractJsonSerializer;


namespace TaxiAutomation.Models
{
    public class Driver : IDriver
    {
        public Driver driver;

        public int DriverId { get; set; }
        public string LicenceNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }
        public bool Available { get; set; }

        public Driver()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["csTaxiAutomation"].ConnectionString;
        }

        public int IsDriverValid(LoginViewModel model)
        {
            string sql = "Select * from Driver where EmailId = @email and Password =@password";
            List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@email", model.UserName),
                        new SqlParameter("@password", model.Password),
                        
                    };
            DataTable dt = new DataTable();
            using (DBDataHelper helper = new DBDataHelper())
            {
                dt = helper.GetDataTable(sql, SQLTextType.Query, lstSqlParam);
            }

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }



        }

        public IDriver Create(Driver d)
        {
            return CreateDriver(d.LicenceNo, d.FirstName, d.LastName, d.Age, d.EmailId, d.ContactNo, d.Password);
        }

        public IDriver Edit(Driver d)
        {
            return EditDriver(d.DriverId, d.LicenceNo, d.FirstName, d.LastName);
        }

        public bool Delete(Driver d)
        {
            return DeleteDriver(d.DriverId);
        }

        //create driver
        public IDriver CreateDriver(string licenceNo, string firstName, string lastName, int? age, string emailId, string contactNo, string password)
        {
            int rowsAffected;
            //check if Driver already exists

            //create new driver row in database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@licenceNo", licenceNo),
                        new SqlParameter("@firstName", firstName),
                        new SqlParameter("@lastName", lastName),
                        new SqlParameter("@age", age),
                        new SqlParameter("@emailId", emailId),
                        new SqlParameter("@contactNo", contactNo),
                        new SqlParameter("@password", password)
                    };

                rowsAffected = helper.GetRowsAffected("dbo.CreateDriver", SQLTextType.Stored_Proc, lstSqlParam);
            }

            if (rowsAffected == 0)
                return null;
            return new Driver().GetDriverByLicenceNo(licenceNo) as Driver;
        }

        //edit driver
        public IDriver EditDriver(int driverId, string licenceNo, string firstName, string lastName)
        {
            int rowsAffected;

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@driverId", driverId),
                        new SqlParameter("@licenceNo", licenceNo),
                        new SqlParameter("@firstName", firstName),
                        new SqlParameter("@lastName", lastName)
                    };

                rowsAffected = helper.GetRowsAffected("dbo.EditDriver", SQLTextType.Stored_Proc, lstSqlParam);
            }

            if (rowsAffected == 0)
                return null;
            return new Driver().GetDriverByLicenceNo(licenceNo) as Driver;
        }

        //delete driver
        public bool DeleteDriver(int driverId)
        {
            bool successStatus = false;
            //delete Driver
            int rowsAffected;

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@driverId", driverId)
                    };

                rowsAffected = helper.GetRowsAffected("dbo.DeleteDriver", SQLTextType.Stored_Proc, lstSqlParam);
            }

            if (rowsAffected == 1)
                successStatus = true;

            return successStatus;
        }

        public int GetIdByEmail(string FirstName)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                Driver d = new Driver();
                List<SqlParameter> list_param = new List<SqlParameter>
                {
                 new SqlParameter("@firstName",FirstName)
                 
                };
                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.ReturnValue;
                list_param.Add(id);
                helper.ExecSQL("dbo.GetDriverId", SQLTextType.Stored_Proc, list_param);
                DriverId = Convert.ToInt32(id.Value);
            };




            return DriverId;
        }

        //get driver by id
        public IDriver GetDriverById(int driverId)
        {
            DataTable dt;
            Driver driver = new Driver();

            //get Driver from database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@driverId", driverId)
                    };

                dt = helper.GetDataTable("dbo.GetDriverById", SQLTextType.Stored_Proc, lstSqlParam);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    driver = new Driver()
                    {
                        DriverId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        LicenceNo = dt.Rows[i]["LicenceNo"].ToString(),
                        FirstName = dt.Rows[i]["FirstName"].ToString(),
                        LastName = dt.Rows[i]["LastName"].ToString(),
                        Age = int.Parse(dt.Rows[i]["Age"].ToString()),
                        EmailId = dt.Rows[i]["EmailId"].ToString(),
                        ContactNo = dt.Rows[i]["ContactNo"].ToString()
                    };
                }
            }

            if (dt == null)
                return null;
            return driver;
        }


        /// <summary>
        /// get driver by licence no
        /// </summary>
        /// <param name="licenceNo"></param>
        /// <returns></returns>
        public IDriver GetDriverByLicenceNo(string licenceNo)
        {
            DataTable dt;
            Driver driver = new Driver();

            //get Driver from database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@licenceNo", licenceNo)
                    };

                dt = helper.GetDataTable("dbo.GetDriverByLicenceNo", SQLTextType.Stored_Proc, lstSqlParam);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    driver = new Driver()
                    {
                        DriverId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        LicenceNo = dt.Rows[i]["LicenceNo"].ToString(),
                        FirstName = dt.Rows[i]["FirstName"].ToString(),
                        LastName = dt.Rows[i]["LastName"].ToString(),
                        Age = int.Parse(dt.Rows[i]["Age"].ToString()),
                        EmailId = dt.Rows[i]["EmailId"].ToString(),
                        ContactNo = dt.Rows[i]["ContactNo"].ToString(),
                        Password = dt.Rows[i]["Password"].ToString()
                    };
                }
            }

            if (dt == null)
                return null;

            return driver;
        }

        /// <summary>
        /// updates the status of the driver 
        /// </summary>
        /// <param name="d"></param>
        public void EnterStatus(Driver d)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>() 
                {
                    new SqlParameter("@available",d.Available),
                    new SqlParameter("@driverid",d.DriverId)
                    
                };

                helper.ExecSQL("dbo.EnterStatus", SQLTextType.Stored_Proc, list_param);
            }

        }

        public void EnterBookStatus(Driver d)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>() 
                {
                    new SqlParameter("@bookStatus",d.Available),
                    new SqlParameter("@driverid",d.DriverId)
                    
                };

                helper.ExecSQL("dbo.EnterBookStatus", SQLTextType.Stored_Proc, list_param);
            }

        }



        /// <summary>
        /// driver checks his status
        /// recieves notification containing the details
        /// </summary>
        ///// <param name="d"></param>
        //public void CheckBookStatus(Driver d)
        //{


        //    using (DBDataHelper helper = new DBDataHelper())
        //    {
        //        List<SqlParameter> list_param = new List<SqlParameter>() 
        //        {
        //            new SqlParameter("@driverid",d.DriverId)
        //        };

        //        SqlParameter booked = new SqlParameter("@booked", SqlDbType.Bit);
        //        SqlParameter vehicleno = new SqlParameter("@vehicleno", SqlDbType.Int);     //checks if a vehicle is booked or not
        //        booked.Direction = ParameterDirection.ReturnValue;
        //        vehicleno.Direction = ParameterDirection.ReturnValue;
        //        list_param.Add(booked);
        //        list_param.Add(vehicleno);
        //        helper.ExecSQL("dbo.CheckBookStatus", SQLTextType.Stored_Proc, list_param);

        //        bool Booked = Convert.ToBoolean(booked.Value);
        //        int vehicleNo = Convert.ToInt32(vehicleno.Value);

        //        string status;

        //        if (Booked == true)
        //        {
        //            status = "Booked";

        //            using (DBDataHelper helper1 = new DBDataHelper())
        //            {
        //                List<SqlParameter> list_param1 = new List<SqlParameter>() 
        //                {                                                                   //gets the customerid for a corresponding booked vehicle
        //                     new SqlParameter("@transportid",vehicleNo)
        //                };
        //                SqlParameter customerid = new SqlParameter("@customerid",SqlDbType.Int);

        //                customerid.Direction = ParameterDirection.ReturnValue;
        //                list_param1.Add(customerid);
        //                helper1.ExecSQL("@dbo.CustomerForDriver",SQLTextType.Stored_Proc,list_param1);



        //                List<SqlParameter> list_param2 = new List<SqlParameter> 
        //                {

        //                    new SqlParameter("@customerid",customerid)
        //                };                                                                      //gets the details of customer for driver
        //                SqlParameter username = new SqlParameter("@username",SqlDbType.VarChar);
        //                SqlParameter boardinglongitude = new SqlParameter("@boardinglongitude", SqlDbType.Float);
        //                SqlParameter boardinglatitude = new SqlParameter("@boardinglatitude", SqlDbType.VarChar);
        //                SqlParameter destinationlongitude = new SqlParameter("@destinationlongitude", SqlDbType.VarChar);
        //                SqlParameter destinationlatitude = new SqlParameter("@destinationlatitude", SqlDbType.VarChar);
        //                SqlParameter contactno = new SqlParameter("@contactno",SqlDbType.Int);

        //                username.Direction = ParameterDirection.ReturnValue;
        //                boardinglongitude.Direction = ParameterDirection.ReturnValue;
        //                boardinglatitude.Direction = ParameterDirection.ReturnValue;
        //                destinationlongitude.Direction = ParameterDirection.ReturnValue;
        //                destinationlatitude.Direction = ParameterDirection.ReturnValue;
        //                contactno.Direction = ParameterDirection.ReturnValue;

        //                list_param2.Add(username);
        //                list_param2.Add(boardinglongitude);
        //                list_param2.Add(boardinglatitude);
        //                list_param2.Add(destinationlongitude);
        //                list_param2.Add(destinationlatitude);
        //                list_param2.Add(contactno);

        //                helper1.ExecSQL("@dbo.GetCustomerBookingDetails", SQLTextType.Stored_Proc, list_param2);

        //            }

        //        }
        //        else
        //        {
        //            status = "Unbooked";
        //            string json = JsonConvert.SerializeObject(status);                          //shows the status as unbooked to the driver  
        //        }


        //    }
        //}


        public string CheckBookStatus(Driver d)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>() 
                {
                    new SqlParameter("@driverid",d.DriverId)
                };

                DataTable dt = helper.GetDataTable("dbo.CheckBookStatus", SQLTextType.Stored_Proc, list_param);

                List<BookingDetail> lstBookingDetails = new List<BookingDetail>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstBookingDetails.Add(new BookingDetail()
                    {
                        Cost = dt.Rows[i]["Cost"].ToString(),
                        NoOfVehicles = dt.Rows[i]["NoOfVehicles"].ToString(),
                        TimeStamp = dt.Rows[i]["TimeStamp"].ToString(),
                        BoardingLatitude = dt.Rows[i]["BoardingLatitude"].ToString(),
                        BoardingLongitude = dt.Rows[i]["BoardingLongitude"].ToString(),
                        DestinationLatitude = dt.Rows[i]["DestinationLatitude"].ToString(),
                        DestinationLongitude = dt.Rows[i]["DestinationLongitude"].ToString(),
                        MobileNo = dt.Rows[i]["MobileNo"].ToString(),
                        EmailId = dt.Rows[i]["EmailId"].ToString(),
                        FirstName = dt.Rows[i]["FirstName"].ToString(),
                        LastName = dt.Rows[i]["LastName"].ToString(),
                        LocationLatitude = dt.Rows[i]["LocationLatitude"].ToString(),
                        LocationLongitude = dt.Rows[i]["LocationLongitude"].ToString()
                    });
                }

                return JsonConvert.SerializeObject(lstBookingDetails);
            }
        }


        private class BookingDetail
        {
            public string Cost { get; set; }
            public string NoOfVehicles { get; set; }
            public string TimeStamp { get; set; }
            public string BoardingLatitude { get; set; }
            public string BoardingLongitude { get; set; }
            public string DestinationLatitude { get; set; }
            public string DestinationLongitude { get; set; }
            public string MobileNo { get; set; }
            public string EmailId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string LocationLatitude { get; set; }
            public string LocationLongitude { get; set; }

        }
    }
}