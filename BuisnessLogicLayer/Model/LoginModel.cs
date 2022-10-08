using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Model
{
    public class LoginModel 
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public string AuthToken { get; set; }
        public bool IsOperationSuccessful { get; set; }
        public string Message { get; set; }
    }
}
