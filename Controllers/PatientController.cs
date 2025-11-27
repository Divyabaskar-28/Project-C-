using HMS;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult PatientBindd()
        {

            DataSet DSss = new DataSet();
            DSss = PatientgridDAL();
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        
        public DataSet PatientgridDAL()
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Patient_Grid";
            return data.SelectDataFromDB(sqlcmd);
        }

       

        public JsonResult GetDropallDetails()
        {

            DataSet DSss = new DataSet();
            DSss = GetDropDetailDAL();
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet GetDropDetailDAL()
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "GetAllDropDown";
            return data.SelectDataFromDB(sqlcmd);
        }

        public JsonResult SavePatient(long PatientID, string pat_name, string pat_gender, int pat_age, string pat_blood, string pat_mobile, string pat_disease,int pat_room, int pat_doctor, string pat_admitteddate, string pat_address, int flag)
        {


            // long caseSessionID = Convert.ToInt64(Session["CaseID"]);
            //UserID = Convert.ToInt64(Session["User_ID"]);
            //Student_ID = Convert.ToInt64(Session["Student_ID"]); ;
            DataSet DSss = new DataSet();
            DSss = PatientSaveIUD(PatientID, pat_name, pat_gender, pat_age, pat_blood, pat_mobile, pat_disease, pat_room, pat_doctor, pat_admitteddate, pat_address, flag);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public DataSet PatientSaveIUD(long PatientID, string pat_name, string pat_gender, int pat_age, string pat_blood, string pat_mobile, string pat_disease, int pat_room, int pat_doctor, string pat_admitteddate, string pat_address, int flag)
        {
            if (pat_admitteddate == "")
            {
               
                 pat_admitteddate = "2025-04-06";
            }
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("PId", PatientID);
            sqlcmd.Parameters.AddWithValue("PName", pat_name);
            sqlcmd.Parameters.AddWithValue("PGender", pat_gender);
            sqlcmd.Parameters.AddWithValue("PAgee", pat_age);
            sqlcmd.Parameters.AddWithValue("PBlood", pat_blood);
            sqlcmd.Parameters.AddWithValue("PMobile", pat_mobile);
            sqlcmd.Parameters.AddWithValue("PDisease", pat_disease);
            sqlcmd.Parameters.AddWithValue("PRoom", pat_room);
            sqlcmd.Parameters.AddWithValue("PDoctor", pat_doctor);
            sqlcmd.Parameters.AddWithValue("PAdmitteddate", pat_admitteddate);
            sqlcmd.Parameters.AddWithValue("PAddress", pat_address);
            sqlcmd.Parameters.AddWithValue("Flag", flag);

            //sqlcmd.Parameters.AddWithValue("userid", UserID);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Patient_IUD";
            return data.SelectDataFromDB(sqlcmd);
        }

        //public JsonResult SavePatient(long PatientID, string pat_name, string pat_gender, int pat_age, string pat_blood, int pat_mobile, string pat_disease, string pat_room, string pat_doctor, string pat_admitteddate, string pat_address, int flag)
        //{


        //   // long caseSessionID = Convert.ToInt64(Session["CaseID"]);
        //    //UserID = Convert.ToInt64(Session["User_ID"]);
        //    //Student_ID = Convert.ToInt64(Session["Student_ID"]); ;
        //    DataSet DSss = new DataSet();
        //    DSss = PatientSaveIUD(PatientID, pat_name, pat_gender, pat_age, pat_blood, pat_mobile,pat_disease, pat_room, pat_doctor, pat_admitteddate, pat_address, flag);
        //    string json = JsonConvert.SerializeObject(DSss);
        //    return Json(json, JsonRequestBehavior.AllowGet);

        //}
        //public DataSet PatientSaveIUD(long PatientID, string pat_name, string pat_gender, int pat_age, string pat_blood, int pat_mobile, string pat_disease, int pat_room, int pat_doctor, string pat_admitteddate, string pat_address, int flag)
        //{
        //    MySqlCommand sqlcmd = new MySqlCommand();
        //    sqlcmd.Parameters.AddWithValue("PId", PatientID);
        //    sqlcmd.Parameters.AddWithValue("PName", pat_name);
        //    sqlcmd.Parameters.AddWithValue("PGender", pat_gender);
        //    sqlcmd.Parameters.AddWithValue("PAgee", pat_age);
        //    sqlcmd.Parameters.AddWithValue("PBlood", pat_blood);
        //    sqlcmd.Parameters.AddWithValue("PMobile", pat_mobile);
        //    sqlcmd.Parameters.AddWithValue("PDisease", pat_disease);
        //    sqlcmd.Parameters.AddWithValue("PRoom", pat_room);
        //    sqlcmd.Parameters.AddWithValue("PDoctor", pat_doctor);
        //    sqlcmd.Parameters.AddWithValue("PAdmitteddate", pat_admitteddate);
        //    sqlcmd.Parameters.AddWithValue("PAddress", pat_address);
        //    sqlcmd.Parameters.AddWithValue("Flag", flag);
        //    sqlcmd.Parameters.AddWithValue("userid", UserID);
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    sqlcmd.CommandText = "Patient_IUD";
        //    return data.SelectDataFromDB(sqlcmd);
        //}
        public JsonResult GetDPatientbyID(long PatientID)
        {

            DataSet DSss = new DataSet();
            DSss = GetPatientDAL(PatientID);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet GetPatientDAL(long PatientID)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("PId", PatientID);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Get_Patient_Detail";
            return data.SelectDataFromDB(sqlcmd);
        }

    }
}