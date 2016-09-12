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
using System.Net.Http;


namespace TaxiAutomation.Models
{
    public class Danger
    {
        public double DangerLocationLatitude { get; set; }
        public double DangerLocationLongitude { get; set; }
        public int CustomerId { get; set; }
        public int TransportId { get; set; }
        public DateTime TimeStamp { get; set; }


        public Danger()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["csTaxiAutomation"].ConnectionString;
        }
        /// <summary>
        /// inserts the details in case of danger
        /// </summary>
        /// <param name="d"></param>
        public void CreateDanger(Danger d)
        {
            using (DBDataHelper helper = new DBDataHelper())
            {
                List<SqlParameter> list_param = new List<SqlParameter>() 
                { 
                    new SqlParameter("@customerid",d.CustomerId),
                     new SqlParameter("@transportid",d.TransportId),
                     new SqlParameter("@dangerlocationlatitude",d.DangerLocationLatitude),
                     new SqlParameter("@dangerlocationlongitude",d.DangerLocationLongitude)
                };

                int rowsaffected=helper.GetRowsAffected("dbo.CreateDanger", SQLTextType.Stored_Proc,list_param);
            }
        }




     
    }
}
