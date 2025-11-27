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
    public class DoctorController : Controller
    {
        // GET: Doctor
        DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }
        long UserID = 123;
        public JsonResult doctor_iud(long doctor_ID, string doctor_firstname, string doctor_lastname, string doctor_gender, string doctor_mobile, string doctor_degree, string doctor_specialist, string doctor_address, string doctor_email, string doctor_blood, int flag)
        {
            long caseSessionID = Convert.ToInt64(Session["CaseID"]);
            
            //doctor_ID = Convert.ToInt64(Session["doctor_ID"]); ;
            DataSet DS = new DataSet();
            DS = doctor_dal_iud(doctor_ID, doctor_firstname, doctor_lastname, doctor_gender, doctor_mobile, doctor_degree, doctor_specialist, doctor_address, doctor_email, doctor_blood, flag);
            string json = JsonConvert.SerializeObject(DS);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public DataSet doctor_dal_iud(long doctor_ID, string doctor_firstname, string doctor_lastname, string doctor_gender, string doctor_mobile, string doctor_degree, string doctor_specialist, 
            string doctor_address, string doctor_email, string doctor_blood,  int flag)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("doctorid", doctor_ID);
            sqlcmd.Parameters.AddWithValue("firstname", doctor_firstname);
            sqlcmd.Parameters.AddWithValue("lastname", doctor_lastname);
            sqlcmd.Parameters.AddWithValue("gender", doctor_gender);
            sqlcmd.Parameters.AddWithValue("mobile", doctor_mobile);
            sqlcmd.Parameters.AddWithValue("degree", doctor_degree);
            sqlcmd.Parameters.AddWithValue("specialist", doctor_specialist);
            sqlcmd.Parameters.AddWithValue("address", doctor_address);
            sqlcmd.Parameters.AddWithValue("email", doctor_email);
            sqlcmd.Parameters.AddWithValue("blood", doctor_blood);
            sqlcmd.Parameters.AddWithValue("flag", flag);
            sqlcmd.Parameters.AddWithValue("userid", 1);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Doctor_IUD";
            return data.SelectDataFromDB(sqlcmd);
        }

        public JsonResult GetDetailDoctor(long doctor_ID)
        {
          
            DataSet DS = new DataSet();
            DS = GetDetailDoctor_dal(doctor_ID);
            string json = JsonConvert.SerializeObject(DS);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public JsonResult griddoctor()
        {

            DataSet DSss = new DataSet();
            DSss = griddoctorDAL();
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet griddoctorDAL()
        {
            MySqlCommand sqlcmd = new MySqlCommand();

            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Doctor_Grid";
            return data.SelectDataFromDB(sqlcmd);
        }

        public DataSet GetDetailDoctor_dal(long doctor_ID)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            
            sqlcmd.Parameters.AddWithValue("doctorid", doctor_ID);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "GetDetailDoctor";
            return data.SelectDataFromDB(sqlcmd);
        }

    }
}