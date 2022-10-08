using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace DataAccessLayer
{
   

    public class LiveRepo
    {
        shcs shcs;
        public LiveRepo()
        {
            shcs = new shcs();
        }
        public List<Live> GetAll()
        {
            return shcs.Lives.ToList();
        }

        public Live GetById(int id)
        {
            return shcs.Lives.FirstOrDefault(x=>x.ID ==id);
        }

        public Live delete(int id)
        {
            var live = shcs.Lives.FirstOrDefault(x => x.ID == id);
             shcs.Lives.Remove(live);
            shcs.SaveChanges();
            return live;

        }

        public Live add(Live emp)
        {
            
            shcs.Lives.Add(emp);
            shcs.SaveChanges();
            return emp;
        }

        public Live edit(Live emp)
        {
            shcs.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            shcs.SaveChanges();
            return emp;
        }

        //public Live GetByEmail(string email)
        //{
        //    return shcs.Lives.FirstOrDefault(x => x.Email == email);
        //}
    }
}