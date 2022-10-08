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
    public class AdminController : Controller
    {
        SqlConnection conn = new SqlConnection(@"workstation id=hosteldataB.mssql.somee.com;packet size=4096;user id=moududhostelDB;pwd=M01767508661m;data source=hosteldataB.mssql.somee.com;persist security info=False;initial catalog=hosteldataB");
        //SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-CLHBNO0\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True");
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
                        return RedirectToAction("EditProfile", "Admin");
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

                    return RedirectToAction("EditProfile", "Admin");
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
            else if (Request.Cookies["Login"] != null)
            {
                return View("UploadImage");
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
            else if (Request.Cookies["Login"] != null)
            {
                return View("ChangePassword");
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
                        return RedirectToAction("ChangePassword", "Admin");
                    }
                    else
                    {
                        TempData["message9"] = "Passord And Confirm Passwrod Not Matching";
                        return RedirectToAction("ChangePassword", "Admin");
                    }
                }
                else if (dt.Rows.Count != 1)
                {
                    conn.Close();
                    TempData["message9"] = "Invalid Old Password";
                    return RedirectToAction("ChangePassword", "Admin");
                }
            }
            return View();
        }

        public ActionResult Newaccount()
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
                return View("Newaccount");
            }

            return View();

        }

        //when click on the registation button in ragistation page then this action will be work
        [HttpPost]
        public ActionResult Register2(AccountModels obj)
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select Email from Profile where Email=@email";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@email", obj.email);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {

                    conn.Close();
                    TempData["message11"] = "Email Alreay Exixt";
                    return RedirectToAction("Newaccount", "Admin");
                }
                else if (obj.Gender == null)
                {

                    conn.Close();
                    TempData["message11"] = "Unknown Gender Selection";
                    return RedirectToAction("Newaccount", "Admin");
                }
                else if (obj.AccountType == null)
                {

                    conn.Close();
                    TempData["message11"] = "Unknown Account Type Selection";
                    return RedirectToAction("Newaccount", "Admin");
                }
                else if (obj.blood == null)
                {
                    conn.Close();
                    TempData["message11"] = "Unknown Account Type Selection";
                    return RedirectToAction("Newaccount", "Admin");

                }
                else
                {
                    conn.Close();
                    if (obj.password == obj.cpassword)
                    {
                        cmd = new SqlCommand("insert into Profile(First_Name, Middle_Name, Last_Name, Email, Phone_No, Password, Date_Of_Birth, Address, Gender, Account_Type, Blood) values(@fname,@mname,@lname,@email,@phone,@password,@birthd,@address,@gender,@type,@blood)", conn);
                        //cmd = new SqlCommand("insert into Blood(First_Name, Middle_Name, Last_Name, Email, Phone_No, Date_Of_Birth, Address, Gender, Blood_Group) values(@fname,@mname,@lname,@email,@phone,@birthd,@address,@gender,@blood)", conn);
                        cmd.Parameters.AddWithValue("fname", obj.fname);
                        cmd.Parameters.AddWithValue("mname", obj.mname);
                        cmd.Parameters.AddWithValue("lname", obj.lname);
                        cmd.Parameters.AddWithValue("email", obj.email);
                        cmd.Parameters.AddWithValue("phone", obj.phone_no);
                        cmd.Parameters.AddWithValue("password", obj.password);
                        cmd.Parameters.AddWithValue("birthd", obj.date_of_birth);
                        cmd.Parameters.AddWithValue("address", obj.address);
                        cmd.Parameters.AddWithValue("gender", obj.Gender);
                        cmd.Parameters.AddWithValue("blood", obj.blood);
                        string type = Convert.ToString(obj.AccountType);
                        cmd.Parameters.AddWithValue("type", type);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        TempData["message12"] = "Account create Sucessfully";
                        return RedirectToAction("Newaccount", "Admin");
                    }
                    else
                    {
                        TempData["message11"] = "Password And Confirm Password Not Match";
                        return RedirectToAction("Newaccount", "Admin");
                    }
                }
            }
            return View();
        }

        public ViewResult EditAccount()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Profile> empList = db.Profiles.ToList<Profile>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Profile());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Profiles.Where(x => x.ID == id).FirstOrDefault<Profile>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Profile emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.ID == 0)
                {
                    db.Profiles.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Profile emp = db.Profiles.Where(x => x.ID == id).FirstOrDefault<Profile>();
                db.Profiles.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ViewResult SearchBlood()
        {
            return View();
        }

        public ViewResult ManageBlood()
        {
            return View();
        }

        public ViewResult UpdateEdit()
        {
            return View();
        }

    }
}
