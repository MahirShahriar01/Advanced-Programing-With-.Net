using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DataAccessLayer
{
   

    public class ProfileRepo
    {
        shcs shcs;
        public ProfileRepo()
        {
            shcs = new shcs();
        }
        public List<Profile> GetAll()
        {
            return shcs.Profiles.ToList();
        }

        public Profile GetById(int id)
        {
            return shcs.Profiles.FirstOrDefault(x=>x.ID ==id);
        }

        public Profile delete(int id)
        {
            var profile = shcs.Profiles.FirstOrDefault(x => x.ID == id);
             shcs.Profiles.Remove(profile);
            shcs.SaveChanges();
            return profile;

        }

        public Profile add(Profile emp)
        {
            
            shcs.Profiles.Add(emp);
            shcs.SaveChanges();
            return emp;
        }

        public Profile edit(Profile emp)
        {
            shcs.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            shcs.SaveChanges();
            return emp;
        }

        public Profile GetByEmail(string email)
        {
            return shcs.Profiles.FirstOrDefault(x => x.Email == email);
        }
    }
}