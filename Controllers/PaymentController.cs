using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HMS;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Newtonsoft.Json;

namespace HMS.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        DBExec data = new DBExec();
        long patient_ID;
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Payment_IUD(long Amount,string Paymenttype, long Balanceamount)
        {
            patient_ID = Convert.ToInt64(Session["patientID"]);
            DataSet DSss = new DataSet();
            DSss = PaymentDAL(patient_ID, Amount, Paymenttype, Balanceamount);
            //patient_ID = Convert.ToInt64(DSss.Tables[0].Rows[0]["Patient_id"]);
            patient_ID = Convert.ToInt64(Session["patientID"]);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet PaymentDAL(long patient_ID,long Amount, string Paymenttype, long Balanceamount)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("PPatient_ID", patient_ID);
            sqlcmd.Parameters.AddWithValue("AAmount", Amount);
            sqlcmd.Parameters.AddWithValue("PPaymenttype", Paymenttype);
            sqlcmd.Parameters.AddWithValue("BBalanceamount", Balanceamount);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Payment_IUD";
            return data.SelectDataFromDB(sqlcmd);
        }
    }
}