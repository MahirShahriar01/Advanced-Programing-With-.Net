using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DataAccessLayer
{
   

    public class BillRepo
    {
        shcs shcs;
        public BillRepo()
        {
            shcs = new shcs();
        }
        public List<Bill> GetAll()
        {
            return shcs.Bills.ToList();
        }

        public Bill GetById(int id)
        {
            return shcs.Bills.FirstOrDefault(x=>x.ID ==id);
        }

        public Bill delete(int id)
        {
            var profile = shcs.Bills.FirstOrDefault(x => x.ID == id);
             shcs.Bills.Remove(profile);
            shcs.SaveChanges();
            return profile;

        }

        public Bill add(Bill emp)
        {
            
            shcs.Bills.Add(emp);
            shcs.SaveChanges();
            return emp;
        }

        public Bill edit(Bill emp)
        {
            shcs.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            shcs.SaveChanges();
            return emp;
        }

        public Bill GetByEmail(string email)
        {
            return shcs.Bills.FirstOrDefault(x => x.Email == email);
        }
    }
}