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

   
    public class  BloodGroupController : ApiController
    {
        BloodGroupBLL bloodGroupBLL;
        public BloodGroupController()
        {
            bloodGroupBLL = new BloodGroupBLL();
        }
        public Bgroup Get(int id)
        {
            return bloodGroupBLL.GetById(id);
        }
        public List<Bgroup> GetAll()
        {
            return bloodGroupBLL.GetAll();
        }
       
        public Bgroup delete(int id)
        {
            return bloodGroupBLL.delete(id);
        }

        [HttpPost]
        public Bgroup AddOrEdit(Bgroup emp)
        {
            return bloodGroupBLL.AddOrEdit(emp);
        }

    }
}
