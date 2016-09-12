using DALHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using TaxiAutomation.Models;
using TaxiAutomation.Models.CoreFunctionality;

namespace TaxiAutomation.Models
{
    public enum TransportEnum
    {
        Taxi = 0,
        Autorickshaw = 1
    }

    public class Transport : ITransport
    {
        public int TransportId { get; set; }
        public string RegistrationNo { get; set; }
        public string VehicleNo { get; set; }
        public Driver TransportDriver { get; set; }
        public TransportEnum TransportType { get; set; }
        public Location Location { get; set; }
        public bool Available { get; set; }
        public bool Booked { get; set; }
        public DateTime TimeStamp { get; set; }

        public Transport()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["csTaxiAutomation"].ConnectionString;
            TransportDriver = new Driver();
        }

        public ITransport Create(Transport t)
        {
            return CreateTransport(t.RegistrationNo, t.VehicleNo, t.TransportDriver.DriverId, t.TransportType, t.Location, t.Available, t.Booked);
        }

        public ITransport Edit(Transport t)
        {
            return EditTransport(t.TransportId, t.TransportDriver.DriverId);
        }

        public bool Delete(Transport t)
        {
            return DeleteTransport(t.TransportId);
        }

        public bool UpdateLocation(Transport e)
        {
            return UpdateTransportLocation(e.TransportId, e.Location);
        }

        public bool UpdateTransportLocation(int transportId, Location transportLocation)
        {
            int rowsAffected;

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list = new List<SqlParameter>() 
                { 
                        new SqlParameter("@transportId", transportId),
                        new SqlParameter("@currTransportLocationLatitude", transportLocation.latitude),
                        new SqlParameter("@currTransportLocationLongitude", transportLocation.longitude)
                };

                rowsAffected = helper.GetRowsAffected("dbo.UpdateTransportLocation", SQLTextType.Stored_Proc, list);
            }

            if (rowsAffected == 0)
                return false;
            return true;
        }

        //create transport
        public ITransport CreateTransport(string registrationNo, string vehicleNo, int driverId, TransportEnum transportType, Location location, bool available, bool booked)
        {
            int rowsAffected;
            //check if Transport already exists

            //create new transport row in database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@registrationNo", registrationNo),
                        new SqlParameter("@vehicleNo", vehicleNo),
                        new SqlParameter("@driverId", driverId),
                        new SqlParameter("@transportType", transportType),
                        new SqlParameter("@locationLatitude", location.latitude),
                        new SqlParameter("@locationLongitude", location.longitude),
                        new SqlParameter("@available", available),
                        new SqlParameter("@booked", booked)
                    };

                rowsAffected = helper.GetRowsAffected("dbo.CreateTransport", SQLTextType.Stored_Proc, lstSqlParam);
            }

            if (rowsAffected == 0)
                return null;
            return new Transport().GetTransportByVehicleNo(vehicleNo) as Transport;
        }

        //delete transport
        public bool DeleteTransport(int transportId)
        {
            bool successStatus = false;
            //delete Driver
            int rowsAffected;

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@transportId", transportId)
                    };

                rowsAffected = helper.GetRowsAffected("dbo.DeleteTransport", SQLTextType.Stored_Proc, lstSqlParam);
            }

            if (rowsAffected == 1)
                successStatus = true;

            return successStatus;
        }

        //edit transport
        public ITransport EditTransport(int transportId, int driverId)
        {
            int rowsAffected;

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@transportId", transportId),
                        new SqlParameter("@driverId", driverId)
                    };

                rowsAffected = helper.GetRowsAffected("dbo.EditTransport", SQLTextType.Stored_Proc, lstSqlParam);
            }

            if (rowsAffected == 0)
                return null;
            return new Transport().GetTransportById(transportId) as Transport;
        }

        //get transport by id
        public ITransport GetTransportById(int transportId)
        {
            DataTable dt;
            Transport transport = new Transport();

            //get Transport from database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@transportId", transportId)
                    };

                dt = helper.GetDataTable("dbo.GetTransportById", SQLTextType.Stored_Proc, lstSqlParam);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    transport = new Transport()
                    {
                        RegistrationNo = dt.Rows[i]["RegistrationNo"].ToString(),
                        TransportDriver = new Driver().GetDriverById(int.Parse(dt.Rows[i]["DriverId"].ToString())) as Driver,
                        TransportId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        TransportType = int.Parse(dt.Rows[i]["TransportType"].ToString()) == 0 ? TransportEnum.Taxi : TransportEnum.Autorickshaw,
                        VehicleNo = dt.Rows[i]["VehicleNo"].ToString(),
                        Location = new Location() { latitude = float.Parse(dt.Rows[0]["LocationLatitude"].ToString()), longitude = float.Parse(dt.Rows[0]["LocationLongitude"].ToString()) },
                        Available = bool.Parse(dt.Rows[i]["Available"].ToString()),
                        Booked = bool.Parse(dt.Rows[i]["Booked"].ToString()),
                        TimeStamp = DateTime.Parse(dt.Rows[i]["TimeStamp"].ToString())
                    };
                }
            }

            if (dt == null)
                return null;
            return transport;
        }

        //get transport by vehicle no
        public ITransport GetTransportByVehicleNo(string vehicleNo)
        {
            DataTable dt;
            Transport transport = new Transport();

            //get Transport from database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@vehicleNo", vehicleNo)
                    };

                dt = helper.GetDataTable("dbo.GetTransportByVehicleNo", SQLTextType.Stored_Proc, lstSqlParam);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    transport = new Transport()
                    {
                        RegistrationNo = dt.Rows[i]["RegistrationNo"].ToString(),
                        TransportDriver = new Driver().GetDriverById(int.Parse(dt.Rows[i]["DriverId"].ToString())) as Driver,
                        TransportId = int.Parse(dt.Rows[i]["Id"].ToString()),
                        TransportType = int.Parse(dt.Rows[i]["TransportType"].ToString()) == 0 ? TransportEnum.Taxi : TransportEnum.Autorickshaw,
                        VehicleNo = dt.Rows[i]["VehicleNo"].ToString(),
                        Location = new Location() { latitude = float.Parse(dt.Rows[0]["LocationLatitude"].ToString()), longitude = float.Parse(dt.Rows[0]["LocationLongitude"].ToString()) },
                        Available = bool.Parse(dt.Rows[i]["Available"].ToString()),
                        Booked = bool.Parse(dt.Rows[i]["Booked"].ToString()),
                        TimeStamp = DateTime.Parse(dt.Rows[i]["TimeStamp"].ToString())
                    };
                }
            }

            if (dt == null)
                return null;
            return transport;
        }

        //not used
        //assign taxi to driver
        public ITransport AssignTaxiToDriver(int taxiId, int driverId)
        {
            DataTable dt;
            Transport transport = new Transport();

            //get Transport from database
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> lstSqlParam = new List<SqlParameter>()
                    {
                        new SqlParameter("@taxiId", taxiId),
                        new SqlParameter("@driverId", driverId)
                    };

                SqlParameter sqlParamOut = new SqlParameter();
                sqlParamOut.Direction = ParameterDirection.Output;
                lstSqlParam.Add(sqlParamOut);

                int rowsAffected = helper.GetRowsAffected("dbo.AssignDriverToTaxi", SQLTextType.Stored_Proc, lstSqlParam);

                if (rowsAffected == 0)
                    return null;
                dt = helper.GetDataTable("dbo.GetTransportById", SQLTextType.Stored_Proc, lstSqlParam);
                if (dt.Rows.Count == 0)
                    return null;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                transport = new Transport()
                {
                    RegistrationNo = dt.Rows[0]["RegistrationNo"].ToString(),
                    TransportDriver = new Driver().GetDriverById(int.Parse(dt.Rows[0]["DriverId"].ToString())) as Driver,
                    TransportId = int.Parse(dt.Rows[0]["Id"].ToString()),
                    TransportType = int.Parse(dt.Rows[0]["TransportType"].ToString()) == 0 ? TransportEnum.Taxi : TransportEnum.Autorickshaw,
                    VehicleNo = dt.Rows[0]["VehicleNo"].ToString(),
                    Location = new Location() { latitude = float.Parse(dt.Rows[0]["LocationLatitude"].ToString()), longitude = float.Parse(dt.Rows[0]["LocationLongitude"].ToString()) },
                    Available = bool.Parse(dt.Rows[0]["Available"].ToString()),
                    Booked = bool.Parse(dt.Rows[0]["Booked"].ToString()),
                    TimeStamp = DateTime.ParseExact(dt.Rows[0]["TimeStamp"].ToString(), "yyyy-mm-dd", CultureInfo.InvariantCulture)
                };
                //}
            }

            return transport;
        }




        // inserts the current coordinates of the driver in the transport table

        //public void LocateDriver(Driver d)
        //{
        //    using (DBDataHelper helper = new DBDataHelper())
        //    {
        //        List<SqlParameter> list_param = new List<SqlParameter>() 
        //        {
        //            new SqlParameter("@locationlatitude",d.LocationLatitude),
        //            new SqlParameter("@locationlongitude",d.LoctionLongitude),
        //            new SqlParameter("@driverid",d.DriverId)
        //        };

        //        helper.ExecSQL("dbo.EnterDriverLocation", SQLTextType.Stored_Proc, list_param);
        //    }
        //}
    }
}