using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HMS;
using MimeKit;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace HMS.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        DBExec data = new DBExec();
        public ActionResult Index()
        {
           // sendemail();
                return View();
           
        }

        public void sendemail()
        {
            
        }

        public JsonResult SaveonlinePatient(string pat_name, string pat_gender, int pat_age, string pat_mobile, string pat_Specialist, string pat_appointmentdate)
        {


            // long caseSessionID = Convert.ToInt64(Session["CaseID"]);
            //UserID = Convert.ToInt64(Session["User_ID"]);
            //Student_ID = Convert.ToInt64(Session["Student_ID"]); ;
            DataSet DSss = new DataSet();
            DSss = savePatientonlineIUD(pat_name, pat_gender, pat_age, pat_mobile, pat_Specialist, pat_appointmentdate);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        public DataSet savePatientonlineIUD(string pat_name, string pat_gender, int pat_age, string pat_mobile, string pat_Specialist, string pat_appointmentdate)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("PName", pat_name);
            sqlcmd.Parameters.AddWithValue("PAgee", pat_age);
            sqlcmd.Parameters.AddWithValue("PGender", pat_gender);
            sqlcmd.Parameters.AddWithValue("PMobile", pat_mobile);
            sqlcmd.Parameters.AddWithValue("PSpecialist", pat_Specialist);
            sqlcmd.Parameters.AddWithValue("PAppointmentdate", pat_appointmentdate);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "patient_onlineIUD";
            return data.SelectDataFromDB(sqlcmd);
        }


        public JsonResult SavecontactPatient(string pat_ID, string pat_Name, string pat_gender, string pat_mobile,string pat_email, string pat_Specialist, string pat_dieasenotes)
        {
            string content = "";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("dd.customerinfo@gmail.com");
            mailMessage.To.Add(pat_email);
            mailMessage.Subject = "HMS - Acknowledgment of Your Information";
            content = "Dear " + pat_Name + ",<br/> <br/>This email confirms that we have received your Information.<br/> <br/> We will reach you shortly.<br/> <br/>Thanks & Regards,<br/>HMS Team.";
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("dd.customerinfo@gmail.com", "zhkyappuhrgkwwea");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
                //Console.WriteLine("Email Sent Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return Json("S", JsonRequestBehavior.AllowGet);

        }
    }
}