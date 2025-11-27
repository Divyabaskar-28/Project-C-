using HMS;
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
    public class RoomController : Controller
    {
        // GET: Room
         //long UserID;
         DBExec data = new DBExec();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RoomBindd()
        {

            DataSet DSss = new DataSet();
            DSss = RoomgridDAL();
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet RoomgridDAL()
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Room_Grid";
            return data.SelectDataFromDB(sqlcmd);
        }

        public JsonResult RoomIUDD(long Room_ID, string room_type , int room_rate, int total_rooms, int flag)
        {


            //long caseSessionID = Convert.ToInt64(Session["CaseID"]);
            //UserID = 123;
            long UserID = Convert.ToInt64(Session["User_ID"]); ;
            DataSet DSss = new DataSet();
            DSss = RoomSaveIUD(Room_ID, room_type, room_rate, total_rooms, flag, UserID);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public DataSet RoomSaveIUD(long Room_ID, string room_type,int room_rate, int total_rooms,int flag,long UserID)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("Roomid", Room_ID);
            sqlcmd.Parameters.AddWithValue("RType", room_type);
            sqlcmd.Parameters.AddWithValue("RRate", room_rate);
            sqlcmd.Parameters.AddWithValue("TRooms", total_rooms);
            sqlcmd.Parameters.AddWithValue("Flag", flag);
            sqlcmd.Parameters.AddWithValue("User_id", UserID);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Room_IUD";
            return data.SelectDataFromDB(sqlcmd);
        }

        public JsonResult GGetDetailRoom(long Room_Id)
        {

            DataSet DSss = new DataSet();
            DSss = GetpatientRoom(Room_Id);
            string json = JsonConvert.SerializeObject(DSss);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public DataSet GetpatientRoom(long Room_Id)
        {
            MySqlCommand sqlcmd = new MySqlCommand();
            sqlcmd.Parameters.AddWithValue("RRoom_ID", Room_Id);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Get_Room_Detail";
            return data.SelectDataFromDB(sqlcmd);
        }
    }
}