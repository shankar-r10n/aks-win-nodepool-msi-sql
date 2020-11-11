using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
using NPTester.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System.Threading.Tasks;

namespace NPTester.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region DB Connect experiment right here
           

            string databaseName = "<your DB1 name>"; // Your DB 1 name referred to as Alpha DB in instructions
            string clientId = "6bdea34d-2d5b-4188-a9b2-ef59e77c5135"; // User Assigned MSI - ClientId

            string databaseName2 = "<your DB2 name>"; //DB2 name reffered to as Gamma DB in instructiosn

            string AadInstance = "https://login.windows.net/{0}";
            string ResourceId = "https://database.windows.net/";
            string connectionResult = "Unable to Connect to Database.";
            string connectionResult2 = "Unable to Connect to Database.";
            string debugBuffer = "";

            //Connection string for DB1/Alpha DB per instructions. Populate your DB server name and DB Name accordingly in the string.
            string sqlConnectionString = "Server=<yourDBServerName>.database.windows.net,1433;Database=<DB1Name>;UID=a;Authentication=Active Directory Interactive";

            //Connection string for DB2/Gamma DB per instructions. Populate your DB server name and DB Name accordingly in the string.
            string sqlConnectionString2 = "Server=winnptestdb.database.windows.net,1433;Database=GammaDB;UID=a;Authentication=Active Directory Interactive";

            Environment.SetEnvironmentVariable("AzureServicesAuthConnectionString", $"RunAs=App; AppId={clientId}");


            using (var conn = new SqlConnection(sqlConnectionString))
            {
                try
                {
                    conn.Open();
                    connectionResult = "DB Connection Successful.";
                
                using (var cmd = new SqlCommand("SELECT 1", conn))
                    {
                        var result = cmd.ExecuteScalar();
                        Console.WriteLine(result.ToString());
                    }

                }
                catch (Exception e) { 
                    
                    //Console.WriteLine(e.Message);
                    //Console.WriteLine(e.StackTrace);
                    
                   // throw;
                }
                          
            }


            using (var conn = new SqlConnection(sqlConnectionString2))
            {
                try
                {
                    conn.Open();
                    connectionResult2 = "DB Connection Successful.";

                    using (var cmd = new SqlCommand("SELECT 1", conn))
                    {
                        var result = cmd.ExecuteScalar();
                        Console.WriteLine(result.ToString());
                    }

                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    //Console.WriteLine(e.StackTrace);
                    
                }

            }


            #endregion DB Connect experiment

            Record rcrd = new Record
            {
                DBName = databaseName,
                DBConnectionResult = connectionResult,
                DBName2 = databaseName2,
                DBConnectionResult2 = connectionResult2,
                DebugBuffer = debugBuffer
            };
            ViewBag.Message = rcrd;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

}