using Asp.NETMVCCRUD.Models;
using Hospital_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Management_System.Controllers
{
    public class DoctorController : Controller
    {
        //SqlConnection conn = new SqlConnection(@"workstation id=hosteldataB.mssql.somee.com;packet size=4096;user id=moududhostelDB;pwd=M01767508661m;data source=hosteldataB.mssql.somee.com;persist security info=False;initial catalog=hosteldataB");
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-GECSQ33\SQLEXPRESS;Initial Catalog=shcs;Integrated Security=True");
        SqlCommand cmd;


        //_Admin is a musterpage for admin login panel and this is action loder
        public ActionResult _Admin()
        {
            return View();
        }

        //_Aprofile logout action loder
        [HttpGet]
        public ActionResult logout()
        {
            HttpCookie mycookie = new HttpCookie("Login");
            mycookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(mycookie);
            Session.Remove("Email");
            return RedirectToAction("Login", "Login");
        }

        //_Aprofile logout master pag Home class action loder
        [HttpGet]
        public ActionResult Home()
        {
            if (Request.Cookies["Login"] == null)
            {
                HttpCookie mycookie = new HttpCookie("Login");
                mycookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(mycookie);
                Session.Remove("Email");
                return RedirectToAction("Login", "Login");
            }
            if (Request.Cookies["Login"] != null)
            {

                if (Session["Email"] == null)
                {
                    Session.Remove("Email");
                    TempData["message4"] = "Session End";
                    return RedirectToAction("Login", "Login");
                }
                else if (Session["Email"] != null)
                {

                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        string sqlQuery = "select First_Name, Middle_Name, Last_Name, Email, Phone_No, Date_Of_Birth, Address, Gender, Account_Type, Blood from Profile where Email=@email";
                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        cmd.Parameters.AddWithValue("@email", Session["Email"]);
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<AccountModels> infolist = new List<AccountModels>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AccountModels obj = new AccountModels();
                                obj.fname = reader["First_Name"].ToString();
                                obj.mname = reader["Middle_Name"].ToString();
                                obj.lname = reader["Last_Name"].ToString();
                                obj.email = reader["Email"].ToString();
                                obj.phone_no = reader["Phone_No"].ToString();
                                obj.date_of_birth = reader["Date_Of_Birth"].ToString();
                                obj.address = reader["Address"].ToString();
                                obj.Gender = reader["Gender"].ToString();
                                obj.type = reader["Account_Type"].ToString();
                                obj.blood = reader["Blood"].ToString();
                                infolist.Add(obj);

                            }
                            ViewBag.info = infolist;
                        }
                        conn.Close();
                    }
                }
            }
            return View("Home");
        }



        //_Aprofile logout master pag Home class action loder
        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Request.Cookies["Login"] == null)
            {
                HttpCookie mycookie = new HttpCookie("Login");
                mycookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(mycookie);
                Session.Remove("Email");
                return RedirectToAction("Login", "Login");
            }
            else if (Request.Cookies["Login"] != null)
            {

                if (Session["Email"] == null)
                {
                    Session.Remove("Email");
                    TempData["message4"] = "Session End";
                    return RedirectToAction("Login", "Login");
                }
                if (Session["Email"] != null)
                {

                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        string sqlQuery = "select First_Name, Middle_Name, Last_Name, Email, Phone_No, Date_Of_Birth, Address, Gender, Account_Type, Blood from Profile where Email=@email";
                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        cmd.Parameters.AddWithValue("@email", Session["Email"]);
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<AccountModels> infolist = new List<AccountModels>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AccountModels obj = new AccountModels();
                                obj.fname = reader["First_Name"].ToString();
                                obj.mname = reader["Middle_Name"].ToString();
                                obj.lname = reader["Last_Name"].ToString();
                                obj.email = reader["Email"].ToString();
                                obj.phone_no = reader["Phone_No"].ToString();
                                obj.date_of_birth = reader["Date_Of_Birth"].ToString();
                                obj.address = reader["Address"].ToString();
                                obj.Gender = reader["Gender"].ToString();
                                obj.type = reader["Account_Type"].ToString();
                                obj.blood = reader["Blood"].ToString();
                                infolist.Add(obj);

                            }
                            ViewBag.info = infolist;
                        }
                        conn.Close();
                    }
                }
            }
            return View("EditProfile");
        }




        //when click on the registation button in ragistation page then this action will be work
        [HttpPost]
        public ActionResult Update(AccountModels obj)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select Email, Password from Profile where Email=@email and Password=@password";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@email", Session["Email"]);
                cmd.Parameters.AddWithValue("@password", obj.cpassword);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {

                    conn.Close();
                    if (obj.cpassword == obj.cpassword)
                    {
                        cmd = new SqlCommand("UPDATE Profile SET Last_Name=@lname, Phone_No=@phone,Address=@address WHERE Email=@email", conn);
                        cmd.Parameters.AddWithValue("@lname", obj.lname);
                        string Email = Session["Email"].ToString();
                        cmd.Parameters.AddWithValue("email", Email);
                        cmd.Parameters.AddWithValue("@phone", obj.phone_no);
                        cmd.Parameters.AddWithValue("@address", obj.address);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        TempData["message8"] = "Sucessfully Update";
                        return RedirectToAction("EditProfile", "Doctor");
                    }
                    else
                    {
                        TempData["message7"] = "Something worng";
                    }
                }
                else if (dt.Rows.Count != 1)
                {
                    conn.Close();
                    TempData["message7"] = "Enter Valid Password";

                    return RedirectToAction("EditProfile", "Doctor");
                }
                else if (obj.lname == null)
                {
                    TempData["message7"] = "Must filup Last Name";
                    conn.Close();
                }
                else if (obj.phone_no == null)
                {
                    TempData["message7"] = "Must filup Phone Number";
                    conn.Close();
                }
                else if (obj.address == null)
                {
                    TempData["message7"] = "Must filup Address";
                    conn.Close();
                }
            }
            return View();
        }



        public ActionResult UploadImage()
        {

            return View("UploadImage");
        }



        public ActionResult ChangePassword()
        {

            return View("ChangePassword");
        }

        [HttpPost]
        public ActionResult Applychange(AccountModels obj)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select Email, Password from Profile where Email=@email and Password=@password";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@email", Session["Email"]);
                cmd.Parameters.AddWithValue("@password", obj.opassword);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {

                    conn.Close();
                    if (obj.password == obj.cpassword)
                    {
                        cmd = new SqlCommand("UPDATE Profile SET Password=@pass WHERE Email=@email", conn);
                        string Email = Session["Email"].ToString();
                        cmd.Parameters.AddWithValue("email", Email);
                        cmd.Parameters.AddWithValue("@pass", obj.cpassword);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        TempData["message10"] = "Sucessfully Change";
                        return RedirectToAction("ChangePassword", "Doctor");
                    }
                    else
                    {
                        TempData["message9"] = "Passord And Confirm Passwrod Not Matching";
                        return RedirectToAction("ChangePassword", "Doctor");
                    }
                }
                else if (dt.Rows.Count != 1)
                {
                    conn.Close();
                    TempData["message9"] = "Invalid Old Password";
                    return RedirectToAction("ChangePassword", "Doctor");
                }
            }
            return View();
        }

        public ActionResult Patient()
        {
            return View("Patient");
        }

        public ActionResult GetData1()
        {
            using (DBModel1 db = new DBModel1())
            {
                List<Bill> empList = db.Bills.ToList<Bill>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            using (DBModel2 db = new DBModel2())
            {
                Live emp = db.Lives.Where(x => x.ID == id).FirstOrDefault<Live>();
                db.Lives.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Checkout Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        //when click on the registation button in ragistation page then this action will be work


        public ViewResult PatientSerial()
        {
            return View("PatientSerial");
        }

        public ViewResult LiveSupport()
        {
            return View("LiveSupport");
        }

        public ActionResult GetData2()
        {
            using (DBModel2 db = new DBModel2())
            {
                List<Live> empList = db.Lives.ToList<Live>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ViewResult Send(AccountModels obj)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                cmd = new SqlCommand("INSERT INTO Live(Name, Comment) VALUES(@name, @comment)", conn);
                cmd.Parameters.AddWithValue("@name", obj.fname);
                cmd.Parameters.AddWithValue("@comment", obj.problem);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["message25"] = "Successfully Send";
                
            }
            return View("LiveSupport");
        }





        public ViewResult SearchBlood()
        {
            return View("SearchBlood");
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Profile> empList = db.Profiles.ToList<Profile>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
