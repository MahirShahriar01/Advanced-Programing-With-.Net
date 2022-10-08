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

   
    public class  BillController : ApiController
    {
        BillBLL billBLL;
        public  BillController()
        {
            billBLL = new  BillBLL();
        }
        public  Bill Get(int id)
        {
            return billBLL.GetById(id);
        }
        public List< Bill> GetAll()
        {
            return billBLL.GetAll();
        }
       
        public  Bill delete(int id)
        {
            return billBLL.delete(id);
        }

        [HttpPost]
        public  Bill AddOrEdit( Bill emp)
        {
            return billBLL.AddOrEdit(emp);
        }

    }
}
