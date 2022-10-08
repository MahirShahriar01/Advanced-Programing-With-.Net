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

   
    public class  AddressController : ApiController
    {
        AddressBLL addressBLL;
        public AddressController()
        {
            addressBLL = new  AddressBLL();
        }
        public  Address Get(int id)
        {
            return addressBLL.GetById(id);
        }
        public List< Address> GetAll()
        {
            return addressBLL.GetAll();
        }
       
        public  Address delete(int id)
        {
            return addressBLL.delete(id);
        }

        [HttpPost]
        public  Address AddOrEdit( Address emp)
        {
            return addressBLL.AddOrEdit(emp);
        }

    }
}
