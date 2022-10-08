using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class BloodGroupBLL
    {
        BloodGroupRepo bloodGroupRepo;
        public BloodGroupBLL()
        {
            bloodGroupRepo = new BloodGroupRepo();
        }
        public List<Bgroup> GetAll()
        {
            return bloodGroupRepo.GetAll();
        }

        public Bgroup GetById(int id)
        {
            return bloodGroupRepo.GetById(id);
        }

        public Bgroup delete(int id)
        {
            return bloodGroupRepo.delete(id);
        }

        public Bgroup AddOrEdit(Bgroup emp)
        {
            if (emp.Id == 0)
            {
                return bloodGroupRepo.add(emp);
            }
            else
            {
                return bloodGroupRepo.edit(emp);
            }
        }
    }
}
