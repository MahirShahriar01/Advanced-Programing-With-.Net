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

   
    public class  PatientController : ApiController
    {
        PatientBLL pBLL;
        public PatientController()
        {
            pBLL = new  PatientBLL();
        }
        public  Pinfo Get(int id)
        {
            return pBLL.GetById(id);
        }
        public List< Pinfo> GetAll()
        {
            return pBLL.GetAll();
        }
       
        public  Pinfo delete(int id)
        {
            return pBLL.delete(id);
        }

        [HttpPost]
        public  Pinfo AddOrEdit( Pinfo emp)
        {
            return pBLL.AddOrEdit(emp);
        }

    }
}
