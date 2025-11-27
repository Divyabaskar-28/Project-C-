using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HMS;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace HMS.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        DBExec data = new DBExec();
        long patient_ID;
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPatientMobile(long MMobile)
        {

            DataSet DSss = new DataSet();
            DSss = GetPatientDetailsDAL(MMobile);
            patient_ID = Convert.ToInt64(DSss.Tables[0].Rows[0]["Patient_id"]);
            Session["patientID"] = patient_ID;
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet GetPatientDetailsDAL(long Mobile)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("MobileNo", Mobile);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "GetPatientDetails";
            return data.SelectDataFromDB(sqlcmd);
        }

        public JsonResult Bill_IUD(int Doctor_value,int Medical_value,int Room_value, int Total_value)
        {

            DataSet DSss = new DataSet();
            DSss = GetBillDAL(Doctor_value, Medical_value, Room_value, Total_value);             
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet GetBillDAL(int Doctorvalue, int Medicalvalue, int Roomvalue, int Totalvalue)
        {
            patient_ID= Convert.ToInt32(Session["patientID"]);
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("Patientid", patient_ID);
            sqlcmd.Parameters.AddWithValue("DoctorFees", Doctorvalue);
            sqlcmd.Parameters.AddWithValue("MedicalFees", Medicalvalue);
            sqlcmd.Parameters.AddWithValue("RoomFees", Roomvalue);
            sqlcmd.Parameters.AddWithValue("TotalFees", Totalvalue);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Bill_IUD";
            return data.SelectDataFromDB(sqlcmd);
        }
    }
}