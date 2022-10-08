using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DataAccessLayer
{
   

    public class PRepo
    {
        shcs shcs;
        public PRepo()
        {
            shcs = new shcs();
        }
        public List<Pinfo> GetAll()
        {
            return shcs.Pinfoes.ToList();
        }

        public Pinfo GetById(int id)
        {
            return shcs.Pinfoes.FirstOrDefault(x=>x.Id ==id);
        }

        public Pinfo delete(int id)
        {
            var pinfo = shcs.Pinfoes.FirstOrDefault(x => x.Id == id);
             shcs.Pinfoes.Remove(pinfo);
            shcs.SaveChanges();
            return pinfo;

        }

        public Pinfo add(Pinfo emp)
        {
            
            shcs.Pinfoes.Add(emp);
            shcs.SaveChanges();
            return emp;
        }

        public Pinfo edit(Pinfo emp)
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