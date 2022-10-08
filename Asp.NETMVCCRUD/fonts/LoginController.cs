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
    public class LoginController : Controller
    {

        SqlConnection conn = new SqlConnection(@"workstation id=hosteldataB.mssql.somee.com;packet size=4096;user id=moududhostelDB;pwd=M01767508661m;data source=hosteldataB.mssql.somee.com;persist security info=False;initial catalog=hosteldataB");
        //SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-CLHBNO0\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True");
        SqlCommand cmd;
        //
        // GET: /Login/

        //---------------------------------------------------------------------------------------------------------------------


        //Home Page Loder
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        //---------------------------------------------------------------------------------------------------------------------


        //Register Page loder
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        //when click on the registation button in ragistation page then this action will be work
        [HttpPost]
        public ActionResult Register(AccountModels obj)
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
                    TempData["message"] = "Email Alreay Exixt";
                    conn.Close();
                }
                else if (obj.Gender == null)
                {
                    TempData["message"] = "Unknown Gender Selection";
                    conn.Close();
                }
                else if (obj.AccountType == null)
                {
                    TempData["message"] = "Unknown Account Type Selection";
                    conn.Close();
                }
                else if (obj.blood == null)
                {
                    TempData["message"] = "Unknown Account Type Selection";
                    conn.Close();
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
                        TempData["message3"] = "Account create Sucessfully";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        TempData["message"] = "Password And Confirm Password Not Match";
                    }
                }
            }
            return View();
        }

        //---------------------------------------------------------------------------------------------------------------------


        //Login page loder
        [HttpGet]
        public ActionResult Login()
        {
            //if find any session and try to load login page then this page redirect to after login page
            if (Session["Email"] != null)
            {
                //if your session is a admin id
                string type = Session["Type"].ToString();
                if (type == "Admin")
                {
                    return RedirectToAction("Home", "Admin");
                }
                else if (type == "Accountant")
                {
                    return RedirectToAction("Home", "Accountant");
                }
                else if (type == "Doctor")
                {
                    return RedirectToAction("Home", "Doctor");
                }
                else if (type == "Patient")
                {
                    return RedirectToAction("Home", "Patient");
                }
                else if (type == "Blood_Donor")
                {
                    return RedirectToAction("Home", "User");
                }
                else if (type == "User")
                {
                    return RedirectToAction("Home", "User");
                }
            }
            return View();
        }


        //After cliking the login button the stystem check your Email and password is valid or invald and so more
        [HttpPost]
        public ActionResult Check(AccountModels obj)
        {


            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sqlQuery = "select Email, Password, Account_Type from Profile where Email=@email and Password=@password";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@email", obj.email);
                cmd.Parameters.AddWithValue("@password", obj.password);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    string type = dt.Rows[0]["Account_Type"].ToString();
                    Session["Type"] = type;

                    if (type == "Admin")
                    {

                        HttpCookie mycookie = new HttpCookie("Login");
                        mycookie["Email"] = dt.Rows[0]["Email"].ToString();
                        mycookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(mycookie);
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        conn.Close();
                        return RedirectToAction("Home", "Admin");
                        //return RedirectToAction("Path", "Controller");
                    }

                    else if (type == "Doctor")
                    {

                        HttpCookie mycookie = new HttpCookie("Login");
                        mycookie["Email"] = dt.Rows[0]["Email"].ToString();
                        mycookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(mycookie);
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        conn.Close();
                        return RedirectToAction("Home", "Doctor");
                        //return RedirectToAction("Path", "Controller");
                    }

                    else if (type == "Accountant")
                    {

                        HttpCookie mycookie = new HttpCookie("Login");
                        mycookie["Email"] = dt.Rows[0]["Email"].ToString();
                        mycookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(mycookie);
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        conn.Close();
                        return RedirectToAction("Home", "Accountant");
                        //return RedirectToAction("Path", "Controller");
                    }

                    else if (type == "Patient")
                    {

                        HttpCookie mycookie = new HttpCookie("Login");
                        mycookie["Email"] = dt.Rows[0]["Email"].ToString();
                        mycookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(mycookie);
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        conn.Close();
                        return RedirectToAction("Home", "Patient");
                        //return RedirectToAction("Path", "Controller");
                    }

                    else if (type == "User")
                    {

                        HttpCookie mycookie = new HttpCookie("Login");
                        mycookie["Email"] = dt.Rows[0]["Email"].ToString();
                        mycookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(mycookie);
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        conn.Close();
                        return RedirectToAction("Home", "User");
                        //return RedirectToAction("Path", "Controller");
                    }

                    else if (type == "Blood_Donor")
                    {

                        HttpCookie mycookie = new HttpCookie("Login");
                        mycookie["Email"] = dt.Rows[0]["Email"].ToString();
                        mycookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(mycookie);
                        Session["Email"] = dt.Rows[0]["Email"].ToString();
                        conn.Close();
                        return RedirectToAction("Home", "User");
                        //return RedirectToAction("Path", "Controller");
                    }

                    else
                    {
                        conn.Close();
                        TempData["message4"] = "Invalid User Type";
                        return RedirectToAction("Login", "Login");
                        //return RedirectToAction("Path", "Controller");
                    }

                }
                else
                {
                    conn.Close();
                    TempData["message4"] = "Worng Email Or Password";
                    return RedirectToAction("Login", "Login");
                }
            }
            return View();
        }



    }
}
