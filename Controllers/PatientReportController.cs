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
    public class PatientReportController : Controller
    {
        // GET: PatientReport
        DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult PatientReportGrid(string Fromdate, string Todate)
        {
            DataSet DSss = new DataSet();
            DSss = PatientReportDAL(Fromdate, Todate);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public DataSet PatientReportDAL(string Fromdate, string Todate)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("Frmdatee", Fromdate);
            sqlcmd.Parameters.AddWithValue("todatee", Todate);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Patient_Report";
            return data.SelectDataFromDB(sqlcmd);
        }
    }
}