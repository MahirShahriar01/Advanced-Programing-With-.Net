using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DataAccessLayer
{
   

    public class BloodGroupRepo
    {
        shcs shcs;
        public BloodGroupRepo()
        {
            shcs = new shcs();
        }
        public List<Bgroup> GetAll()
        {
            return shcs.Bgroups.ToList();
        }

        public Bgroup GetById(int id)
        {
            return shcs.Bgroups.FirstOrDefault(x=>x.Id == id);
        }

        public Bgroup delete(int id)
        {
            var bloodGroup = shcs.Bgroups.FirstOrDefault(x => x.Id == id);
             shcs.Bgroups.Remove(bloodGroup);
            shcs.SaveChanges();
            return bloodGroup;

        }

        public Bgroup add(Bgroup emp)
        {
            
            shcs.Bgroups.Add(emp);
            shcs.SaveChanges();
            return emp;
        }

        public Bgroup edit(Bgroup emp)
        {
            shcs.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            shcs.SaveChanges();
            return emp;
        }

       
    }
}