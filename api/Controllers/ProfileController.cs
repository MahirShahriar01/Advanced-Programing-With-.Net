using BuisnessLogicLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using api.App_Start;
namespace api.Controllers
{

   
    public class ProfileController : ApiController
    {
        ProfileBLL profileBLL;
        public ProfileController()
        {
            profileBLL = new ProfileBLL();
        }
        public Profile Get(int id)
        {
            return profileBLL.GetById(id);
        }
        public List<Profile> GetAll()
        {
            return profileBLL.GetAll();
        }
       
        public Profile delete(int id)
        {
            return profileBLL.delete(id);
        }

        [HttpPost]
        public Profile AddOrEdit(Profile emp)
        {
            return profileBLL.AddOrEdit(emp);
        }

    }
}
