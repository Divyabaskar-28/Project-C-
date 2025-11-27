using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace HMS.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult OnlineGrid(string Fromdate)
        {
            DataSet DSss = new DataSet();
            DSss = PatientonlineReportDAL(Fromdate);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public DataSet PatientonlineReportDAL(string Fromdate)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("Frmdatee", Fromdate);
            
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Patient_OnlineappointmentReport";
            return data.SelectDataFromDB(sqlcmd);
        }
    }
}