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
using System.Runtime.Serialization;
using System.Net.Http;


namespace TaxiAutomation.Models
{
    public struct Location
    {
        public double latitude;
        public double longitude;
    }


    public class Booking
    {
        double factor = 1;

        public int Id { get; set; }
        public int TransportId { get; set; }
        public int CustomerId { get; set; }
        public double Cost { get; set; }
        public int NoOfVehicles { get; set; }
        public DateTime TimeStamp { get; set; }
        public Location Boarding { get; set; }
        public Location Destination { get; set; }


        public Booking()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["csTaxiAutomation"].ConnectionString;
        }

        /// <summary>
        /// books transport
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Book(Booking b)
        {
            double cost = 0;
            int transportid = 0;

            GetTransportIdAndCost(b, ref cost, ref transportid);

            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>
                {
                    new SqlParameter("@transportid",transportid),
                    new SqlParameter("@customerid",b.CustomerId),
                    new SqlParameter("@noofvehicles",b.NoOfVehicles),
                    new SqlParameter("@cost",cost),
                    new SqlParameter("@boardinglatitude",b.Boarding.latitude),
                    new SqlParameter("@boardinglongitude",b.Boarding.longitude),
                    new SqlParameter("@destinationlatitude",b.Destination.latitude),
                    new SqlParameter("@destinationlongitude",b.Destination.longitude)
                };

                //return BookingId
                int rowsaffected = helper.GetRowsAffected("dbo.AddBooking", SQLTextType.Stored_Proc, list_param);
            }
            return 0;
        }

        private void GetTransportIdAndCost(Booking b, ref double cost, ref int transportid)
        {
            double distance;

            DataTable dt = new DataTable();

            double x = Math.Pow((b.Destination.latitude - b.Boarding.latitude), 2);
            double y = Math.Pow((b.Destination.longitude - b.Boarding.latitude), 2);

            distance = Math.Sqrt(x + y);



            using (DBDataHelper helper = new DBDataHelper())
            {
                dt = helper.GetDataTable("dbo.GetAllTransports", SQLTextType.Stored_Proc);
            }

            double x11 = Convert.ToDouble(dt.Rows[0][5]);
            double x21 = Convert.ToDouble(dt.Rows[0][6]);

            double d11 = Math.Pow((x11 - b.Boarding.latitude), 2);
            double d21 = Math.Pow((x21 - b.Boarding.longitude), 2);

            double d = Math.Sqrt(d21 + d11);

            double min = d;
            transportid = Convert.ToInt32(dt.Rows[0]["Id"]);
            int counter = 0;
            foreach (DataRow dr in dt.Rows)
            {
                //calculate distance b/w boarding loc and taxi loc
                double x1 = Convert.ToDouble(dt.Rows[counter][5]);
                double x2 = Convert.ToDouble(dt.Rows[counter][6]);

                double d1 = Math.Pow((x1 - b.Boarding.latitude), 2);
                double d2 = Math.Pow((x2 - b.Boarding.longitude), 2);

                double dist = Math.Sqrt(d2 + d1);

                //select taxi with min dist + available + !booked
                if ((Convert.ToBoolean(dt.Rows[counter][7])) == true && (Convert.ToBoolean(dt.Rows[counter][8]) == false) && min > dist)
                {
                    min = dist;
                    transportid = Convert.ToInt32(dr["Id"]);

                }
                counter++;

            }
            cost = min * factor;
        }


    }
}
