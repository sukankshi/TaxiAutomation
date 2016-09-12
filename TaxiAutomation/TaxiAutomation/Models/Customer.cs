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
    struct Details
    {
        public int Id;
        public string typeOfUser;
        public string isPresent;

    }

    public class Customer
    {
        public string json;
        public Customer customer;

        public int Id { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmergencyNo { get; set; }

        public Customer()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["csTaxiAutomation"].ConnectionString;
        }


        public int IsCustomerValid(LoginViewModel model)
        {
            string sql = "Select * from Customer where EmailId = @email and Password =@password";
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


        //create Customer
        public int CreateCustomer(Customer c)
        {

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>
                {
                  new SqlParameter("@firstname",c.FirstName),
                  new SqlParameter("@lastname",c.LastName),
                  new SqlParameter("@emailid",c.EmailId),
                  new SqlParameter("@username",c.UserName),
                  new SqlParameter("@mobileno",c.MobileNo),
                  new SqlParameter("@password",c.Password),
                  new SqlParameter("@emergencyno",c.EmergencyNo)
                  
                };

                int rowsaffected = helper.GetRowsAffected("dbo.CreateCustomer", SQLTextType.Stored_Proc, list_param);
            }

            int customId = GetIdByEmail(c.EmailId);


            string json = JsonConvert.SerializeObject(customId);        //convets the variable into json
            return customId;
        }

        /// <summary>
        /// get customer id
        /// by emailid
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        public int GetIdByEmail(string EmailId)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>
                {
                new SqlParameter("@emailId", EmailId)
                };

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.ReturnValue;
                list_param.Add(id);

                helper.ExecSQL("dbo.GetCustomerId", SQLTextType.Stored_Proc, list_param);
                Id = Convert.ToInt32(id.Value);
            };

            return Id;
        }

        /// <summary>
        /// Deletes the customer by emailid
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool DeleteCustomer(Customer c)
        {
            int rowsaffected;
            bool status = false;
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>()
                {
                    new SqlParameter("@emailid",c.EmailId)
                };
                rowsaffected = helper.GetRowsAffected("dbo.DeleteCustomer", SQLTextType.Stored_Proc, list_param);
            }

            if (rowsaffected == 1)
                status = true;

            return status;

        }

        /// <summary>
        /// Edit customeer
        /// parametere
        /// </summary>
        public bool EditCustomer(Customer c)
        {
            bool status = false;
            //int rowsaffected;
            using (DBDataHelper helper = new DBDataHelper())
            {

                List<SqlParameter> list_param = new List<SqlParameter>()
                {
                    new SqlParameter("@mobileno",c.MobileNo),
                    new SqlParameter("@firstname",c.FirstName),
                    new SqlParameter("@lastname",c.LastName),
                    new SqlParameter("@password",c.Password),
                    new SqlParameter("@emergencyno",c.EmergencyNo),
                    new SqlParameter("@emailid",c.EmailId)
                };

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.ReturnValue;
                list_param.Add(id);
                helper.ExecSQL("dbo.EditCustomer", SQLTextType.Stored_Proc, list_param);

                Id = Convert.ToInt32(id.Value);
            }
            if (Id != 0)
            {
                status = true;
            }

            return status;
        }

        public string LoginCustomer(Customer c)
        {
            Details loginDetails = new Details();

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>() 
                {
                    new SqlParameter("@emailid",c.EmailId),
                    new SqlParameter("@password",c.Password)
                };

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.ReturnValue;
                list_param.Add(id);

                helper.ExecSQL("dbo.CustomerLogin", SQLTextType.Stored_Proc, list_param);
                loginDetails.Id = Convert.ToInt32(id.Value);



                if (loginDetails.Id != 0)                                   //checks if the user is customer
                {

                    loginDetails.typeOfUser = "customer";
                    loginDetails.isPresent = "True";
                    json = JsonConvert.SerializeObject(loginDetails);

                }

                else
                {
                    List<SqlParameter> list_param1 = new List<SqlParameter>()
                    {
                         new SqlParameter("@emailid",c.EmailId),
                         new SqlParameter("@password",c.Password)
                    };
                    SqlParameter id1 = new SqlParameter("@id1", SqlDbType.Int);
                    id1.Direction = ParameterDirection.ReturnValue;
                    list_param1.Add(id1);

                    helper.ExecSQL("dbo.GetDriverById", SQLTextType.Stored_Proc, list_param1);
                    loginDetails.Id = Convert.ToInt32(id1.Value);

                    if (loginDetails.Id != 0)                                    //checks if the user is driver
                    {
                        loginDetails.typeOfUser = "driver";
                        loginDetails.isPresent = "True";
                        json = JsonConvert.SerializeObject(loginDetails);

                    }
                    else
                    {
                        loginDetails.isPresent = "False";                           //the user is not present
                        json = JsonConvert.SerializeObject(loginDetails.isPresent);
                    }

                }

                return json;
            }

        }

        //public void KnowBookStatus(Customer c)
        //{
        //    string status;

        //    using (DBDataHelper helper = new DBDataHelper())
        //    {
        //        List<SqlParameter> list_param = new List<SqlParameter>() 
        //            { 
        //            new SqlParameter("@customerid",c.Id)
        //            };

        //        SqlParameter driverid = new SqlParameter("@driverid", SqlDbType.VarChar);
        //        driverid.Direction = ParameterDirection.ReturnValue;

        //        SqlParameter booked = new SqlParameter("@booked", SqlDbType.Bit);
        //        booked.Direction = ParameterDirection.ReturnValue;

        //        list_param.Add(driverid);
        //        list_param.Add(booked);

        //        DataTable dt = helper.GetDataTable("dbo.DriverForCustomer", SQLTextType.Stored_Proc, list_param);
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            int driverId = Convert.ToInt32(dt.Rows[i]["DriverId"]);
        //            bool Booked = Convert.ToBoolean(dt.Rows[i]["Booked"]);

        //            if (Booked == true)
        //            {
        //                status = "Booked";

        //                List<SqlParameter> list_param1 = new List<SqlParameter>() 
        //                {
        //                new SqlParameter("@driverid",driverId),
        //                new SqlParameter("@customerid",c.Id)
        //                };

        //                SqlParameter drivername = new SqlParameter("@drivername", SqlDbType.Int);
        //                drivername.Direction = ParameterDirection.ReturnValue;

        //                SqlParameter taxino = new SqlParameter("@transportid", SqlDbType.Int);
        //                taxino.Direction = ParameterDirection.ReturnValue;

        //                SqlParameter contactno = new SqlParameter("@contactno", SqlDbType.Int);
        //                contactno.Direction = ParameterDirection.ReturnValue;

        //                SqlParameter cost = new SqlParameter("@cost", SqlDbType.Float);
        //                cost.Direction = ParameterDirection.ReturnValue;

        //                helper.ExecSQL("dbo.GetDriverDetails", SQLTextType.Stored_Proc, list_param1);



        //                string json = JsonConvert.SerializeObject(status);
        //                string json1 = JsonConvert.SerializeObject(drivername);
        //                string json2 = JsonConvert.SerializeObject(taxino);
        //                string json3 = JsonConvert.SerializeObject(contactno);
        //                string json4 = JsonConvert.SerializeObject(cost);

        //            }
        //            else
        //            {
        //                status = "Under Processing";
        //                string json = JsonConvert.SerializeObject(status);

        //            }
        //        }

        //        //int driverId = Convert.ToInt32(driverid.Value);
        //        //bool Booked = Convert.ToBoolean(booked.Value);

        //        //if (Booked == true)
        //        //{
        //        //    status = "Booked";

        //        //    List<SqlParameter> list_param1 = new List<SqlParameter>() 
        //        //        {
        //        //        new SqlParameter("@driverid",driverId),
        //        //        new SqlParameter("@customerid",c.Id)
        //        //        };

        //        //    SqlParameter drivername = new SqlParameter("@drivername", SqlDbType.Int);
        //        //    drivername.Direction = ParameterDirection.ReturnValue;

        //        //    SqlParameter taxino = new SqlParameter("@transportid", SqlDbType.Int);
        //        //    taxino.Direction = ParameterDirection.ReturnValue;

        //        //    SqlParameter contactno = new SqlParameter("@contactno", SqlDbType.Int);
        //        //    contactno.Direction = ParameterDirection.ReturnValue;

        //        //    SqlParameter cost = new SqlParameter("@cost", SqlDbType.Float);
        //        //    cost.Direction = ParameterDirection.ReturnValue;

        //        //    helper.ExecSQL("dbo.GetDriverDetails", SQLTextType.Stored_Proc, list_param1);



        //        //    string json = JsonConvert.SerializeObject(status);
        //        //    string json1 = JsonConvert.SerializeObject(drivername);
        //        //    string json2 = JsonConvert.SerializeObject(taxino);
        //        //    string json3 = JsonConvert.SerializeObject(contactno);
        //        //    string json4 = JsonConvert.SerializeObject(cost);

        //        //}
        //        //else
        //        //{
        //        //    status = "Under Processing";
        //        //    string json = JsonConvert.SerializeObject(status);

        //        //}
        //    }


        //}

        public string KnowBookStatus(Customer c)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>() 
                    { 
                    new SqlParameter("@customerid",c.Id)
                    };

                DataTable dt = helper.GetDataTable("dbo.GetCustomerBookingDetails", SQLTextType.Stored_Proc, list_param);

                List<BookingDetail> lstBookingDetails = new List<BookingDetail>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstBookingDetails.Add(new BookingDetail() { Cost = dt.Rows[i]["Cost"].ToString(), NoOfVehicles = dt.Rows[i]["NoOfVehicles"].ToString(), TimeStamp = dt.Rows[i]["TimeStamp"].ToString(), BoardingLatitude = dt.Rows[i]["BoardingLatitude"].ToString(), BoardingLongitude = dt.Rows[i]["BoardingLongitude"].ToString(), DestinationLatitude = dt.Rows[i]["DestinationLatitude"].ToString(), DestinationLongitude = dt.Rows[i]["DestinationLongitude"].ToString(), LicenceNo = dt.Rows[i]["LicenceNo"].ToString(), FirstName = dt.Rows[i]["FirstName"].ToString(), LastName = dt.Rows[i]["LastName"].ToString(), Age = dt.Rows[i]["Age"].ToString(), ContactNo = dt.Rows[i]["ContactNo"].ToString(), VehicleNo = dt.Rows[i]["VehicleNo"].ToString(), TransportType = dt.Rows[i]["TransportType"].ToString(), LocationLatitude = dt.Rows[i]["LocationLatitude"].ToString(), LocationLongitude = dt.Rows[i]["LocationLongitude"].ToString() });
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
            public string LicenceNo { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Age { get; set; }
            public string ContactNo { get; set; }
            public string VehicleNo { get; set; }
            public string TransportType { get; set; }
            public string LocationLatitude { get; set; }
            public string LocationLongitude { get; set; }
        }

    }
}
