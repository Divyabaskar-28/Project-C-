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
    public class DischargeController : Controller
    {
        // GET: Discharge
        DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DischargeBindd()
        {

            DataSet DSss = new DataSet();
            DSss = DischargegridDAL();
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet DischargegridDAL()
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Discharge_Grid";
            return data.SelectDataFromDB(sqlcmd);
        }

        public JsonResult DischargePatient(long PPatientID, long RegID)
        {


            // long caseSessionID = Convert.ToInt64(Session["CaseID"]);
            //UserID = Convert.ToInt64(Session["User_ID"]);
            //Student_ID = Convert.ToInt64(Session["Student_ID"]); ;
            DataSet DSss = new DataSet();
            DSss = DischargeIUD(PPatientID, RegID);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public DataSet DischargeIUD(long PatientID, long RegID)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("PPatientID", PatientID);
            sqlcmd.Parameters.AddWithValue("PRegisterID", RegID);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "DischargeIUD";
            return data.SelectDataFromDB(sqlcmd);
        }

    }
}