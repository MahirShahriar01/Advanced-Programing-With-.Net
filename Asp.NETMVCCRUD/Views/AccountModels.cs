using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public class AccountModels
    {
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string phone_no { get; set; }
        public string opassword { get; set; }
        public string password { get; set; }
        public string cpassword { get; set; }
        public string date_of_birth { get; set; }
        public string address { get; set; }
        public string Gender { get; set; }
        public string blood { get; set; }

        public string type { get; set; }
        public account_type AccountType { get; set; }
        public enum account_type
        {
            User,
            Blood_Donor,
            Patient,
            Doctor,
            Accountant,
            Admin
        }

        public string problem { get; set; }
        public string room_no { get; set; }
        public room Room { get; set; }
        public enum room
        {
            R101,
            R102,
            R103,
            R104,
            R105,
            R106,
            R107,
            R108,
            R109,
            R110,
            R201,
            R202,
            R203,
            R204,
            R205,
            R206,
            R207,
            R208,
            R209,
            R210
        }

        public string session { get; set; }
        public string id { get; set; }
        public string payable { get; set; }
        public string paid { get; set; }
        public string due { get; set; }
        public string date { get; set; }
    }
}