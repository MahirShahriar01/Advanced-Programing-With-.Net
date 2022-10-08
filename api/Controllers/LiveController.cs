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

   
    public class  LiveController : ApiController
    {
        LiveBLL liveBLL;
        public  LiveController()
        {
            liveBLL = new  LiveBLL();
        }
        public  Live Get(int id)
        {
            return liveBLL.GetById(id);
        }
        public List< Live> GetAll()
        {
            return liveBLL.GetAll();
        }
       
        public  Live delete(int id)
        {
            return liveBLL.delete(id);
        }

        [HttpPost]
        public  Live AddOrEdit( Live emp)
        {
            return liveBLL.AddOrEdit(emp);
        }

    }
}
