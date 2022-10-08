using BuisnessLogicLayer;
using BuisnessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class AccountController : ApiController
    {
        LoginService loginService;
        public AccountController()
        {
            loginService = new LoginService();
        }
        [HttpPost]
        public LoginModel LoginCredentialVerification(LoginModel model)
        {
            return loginService.LoginCredentialVerification(model);
        }
    }
}
