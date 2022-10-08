using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DataAccessLayer
{
   

    public class AddressRepo
    {
        shcs shcs;
        public AddressRepo()
        {
            shcs = new shcs();
        }
        public List<Address> GetAll()
        {
            return shcs.Addresses.ToList();
        }

        public Address GetById(int id)
        {
            return shcs.Addresses.FirstOrDefault(x=>x.Id ==id);
        }

        public Address delete(int id)
        {
            var address = shcs.Addresses.FirstOrDefault(x => x.Id == id);
             shcs.Addresses.Remove(address);
            shcs.SaveChanges();
            return address;

        }

        public Address add(Address emp)
        {
            
            shcs.Addresses.Add(emp);
            shcs.SaveChanges();
            return emp;
        }

        public Address edit(Address emp)
        {
            shcs.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            shcs.SaveChanges();
            return emp;
        }

        //public Address GetByEmail(string email)
        //{
        //    return shcs.Addresses.FirstOrDefault(x => x.Email == email);
        //}
    }
}