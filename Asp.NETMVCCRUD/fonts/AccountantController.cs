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
    public class AccountantController : Controller
    {
        SqlConnection conn = new SqlConnection(@"workstation id=hosteldataB.mssql.somee.com;packet size=4096;user id=moududhostelDB;pwd=M01767508661m;data source=hosteldataB.mssql.somee.com;persist security info=False;initial catalog=hosteldataB");
        //SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-CLHBNO0\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True");
        SqlCommand cmd;


        //_Admin is a musterpage for admin login panel and this is action loder
        public ActionResult _Accountant()
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
                    TempData["message4"] = "At First Enter Valid Email And Password";
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
                        return RedirectToAction("EditProfile", "Accountant");
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

                    return RedirectToAction("EditProfile", "Accountant");
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
            if (Request.Cookies["Login"] == null)
            {
                HttpCookie mycookie = new HttpCookie("Login");
                mycookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(mycookie);
                Session.Remove("Email");
                return RedirectToAction("Login", "Login");
            }
            return View();
        }



        public ActionResult ChangePassword()
        {
            if (Request.Cookies["Login"] == null)
            {
                HttpCookie mycookie = new HttpCookie("Login");
                mycookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(mycookie);
                Session.Remove("Email");
                return RedirectToAction("Login", "Login");
            }
            return View();
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
                        return RedirectToAction("ChangePassword", "Accountant");
                    }
                    else
                    {
                        TempData["message9"] = "Passord And Confirm Passwrod Not Matching";
                        return RedirectToAction("ChangePassword", "Accountant");
                    }
                }
                else if (dt.Rows.Count != 1)
                {
                    conn.Close();
                    TempData["message9"] = "Invalid Old Password";
                    return RedirectToAction("ChangePassword", "Accountant");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult PatientBill()
        {
            return View("PatientBill");
        }


        //[HttpPost]
        //public ActionResult PatientBill(AccountModels obj2)
        //{

        //    conn.Open();
        //    if (conn.State == System.Data.ConnectionState.Open)
        //    {
        //        string sqlQuery = "select Email, Payable, Paid, Due from Bill where ID=@id";
        //        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        //        cmd.Parameters.AddWithValue("@id", obj2.id);
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        List<AccountModels> infolist = new List<AccountModels>();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                AccountModels obj = new AccountModels();
        //                obj.email = reader["Email"].ToString();
        //                obj.payable = reader["Payable"].ToString();
        //                obj.paid = reader["Paid"].ToString();
        //                obj.due = reader["Due"].ToString();
        //                infolist.Add(obj);

        //            }
        //            ViewBag.info = infolist;
        //        }
        //        conn.Close();
        //    }
        //    return View();
        //}
        [HttpGet]
        public ActionResult SearchPatient()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SearchResult(AccountModels obj2)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select AID from Bill where AID=@aid";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@aid", obj2.id);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        string sqlQuery3 = "select Email, Payable, Paid, Due, AID, Date, Bed_No, Problem from Bill where AID=@id";
                        SqlCommand cmd2 = new SqlCommand(sqlQuery3, conn);
                        cmd2.Parameters.AddWithValue("@id", obj2.id);
                        SqlDataReader reader = cmd2.ExecuteReader();

                        List<AccountModels> infolist = new List<AccountModels>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AccountModels obj = new AccountModels();
                                obj.email = reader["Email"].ToString();
                                obj.payable = reader["Payable"].ToString();
                                obj.paid = reader["Paid"].ToString();
                                obj.due = reader["Due"].ToString();
                                obj.id = reader["AID"].ToString();
                                obj.date = reader["Date"].ToString();
                                obj.room_no = reader["Bed_No"].ToString();
                                obj.problem = reader["Problem"].ToString();
                                infolist.Add(obj);

                            }
                            ViewBag.info = infolist;
                        }
                        conn.Close();
                        return View();
                    } 
                }
                else
                {
                    conn.Close();
                    TempData["message20"] = "No Admited Patient.";
                    return RedirectToAction("SearchPatient", "Accountant");
                }
               
            }
            return View();
        }

        [HttpPost]
        public ActionResult billupdate(AccountModels obj)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                cmd = new SqlCommand("UPDATE Bill SET Payable=@payable, Paid=@paid, Due=@due WHERE AID=@aid", conn);
                //float payable = Convert.ToInt32(obj.payable);
                //float paid = Convert.ToInt32(obj.paid);
                //float due = Convert.ToInt32(obj.due);
                cmd.Parameters.AddWithValue("@aid", obj.id);
                cmd.Parameters.AddWithValue("@payable", obj.payable);
                cmd.Parameters.AddWithValue("@paid", obj.paid);
                cmd.Parameters.AddWithValue("@due", obj.due);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["message21"] = "Bill Sucessfully Update";
                return RedirectToAction("SearchPatient", "Accountant");
            }
            return View();
        }

        [HttpGet]
        public ActionResult NewPatient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyPatient(AccountModels obj2)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select ID from Profile where ID=@id and Account_Type='Patient'";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@id", obj2.id);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        string sqlQuery3 = "select ID, Email from Profile where ID=@id";
                        SqlCommand cmd2 = new SqlCommand(sqlQuery3, conn);
                        cmd2.Parameters.AddWithValue("@id", obj2.id);
                        SqlDataReader reader = cmd2.ExecuteReader();

                        List<AccountModels> infolist = new List<AccountModels>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AccountModels obj = new AccountModels();
                                obj.email = reader["Email"].ToString();
                                obj.id = reader["ID"].ToString();
                                infolist.Add(obj);

                            }
                            ViewBag.info = infolist;
                        }
                        conn.Close();
                        return View();
                    }
                }
                else
                {
                    conn.Close();
                    TempData["message22"] = "No Patient Account For This ID.";
                    return RedirectToAction("NewPatient", "Accountant");
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult AddBillList(AccountModels obj)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select AID, Bed_No from Bill where AID=@aid or Bed_No=@bed";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@aid", obj.id);
                string room = Convert.ToString(obj.Room);
                cmd.Parameters.AddWithValue("@bed", room);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    conn.Close();
                    TempData["message22"] = "This Patient/Room Already Admited";
                    return RedirectToAction("NewPatient", "Accountant");

                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Bill(Email, Payable, Paid, Due, AID, Date, Bed_No,  Problem) VALUES(@email,@payable,@paid,@due,@aid,@date,@bed,@problem)", conn);
                    cmd.Parameters.AddWithValue("@email", obj.email);
                    cmd.Parameters.AddWithValue("@aid", obj.id);
                    cmd.Parameters.AddWithValue("@payable", obj.payable);
                    cmd.Parameters.AddWithValue("@paid", obj.paid);
                    cmd.Parameters.AddWithValue("@due", obj.due);
                    cmd.Parameters.AddWithValue("@date", obj.date);
                    string room1 = Convert.ToString(obj.Room);
                    cmd.Parameters.AddWithValue("@bed", room1);
                    cmd.Parameters.AddWithValue("@problem", obj.problem);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    TempData["message23"] = "Patient Add Sucessful";
                    return RedirectToAction("NewPatient", "Accountant");
                }
            }
            return View();
        }

        
        //when click on the registation button in ragistation page then this action will be work


        public ViewResult TotalBalence()
        {
            return View("TotalBalence");
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
        public ActionResult Delete(int id)
        {
            using (DBModel1 db = new DBModel1())
            {
                Bill emp = db.Bills.Where(x => x.ID == id).FirstOrDefault<Bill>();
                db.Bills.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Checkout Successfully" }, JsonRequestBehavior.AllowGet);
            }
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
