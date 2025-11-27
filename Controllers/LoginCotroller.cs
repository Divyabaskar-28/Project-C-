using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class loginController : Controller
    {
        DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult UserLogin(string Username, string password)
        {
            if (Username.Trim() == "")
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
            else if (password.Trim() == "")
            {
                return Json("P", JsonRequestBehavior.AllowGet);
            }

            string username = Username.Trim();
            string pass = password.Trim().Replace(" ", "");


            DataSet DSss = LoginUser(username, pass);
            string json = JsonConvert.SerializeObject(DSss);
            if (DSss.Tables.Count > 0)
            {
                if (DSss.Tables[0].Rows.Count > 0)
                {
                    if (DSss.Tables[0].Rows[0]["Result"].ToString() == "S")
                    {
                        Session["User_ID"] = DSss.Tables[0].Rows[0]["user_id"].ToString();
                    }
                }
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet LoginUser(string username, string password)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("@uusername", username);
            sqlcmd.Parameters.AddWithValue("@upass", password);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "User_Login";
            return data.SelectDataFromDB(sqlcmd);
        }
    }
}