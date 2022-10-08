using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class PatientBLL
    {
        PRepo pRepo;
        public PatientBLL()
        {
            pRepo = new PRepo();
        }
        public List<Pinfo> GetAll()
        {
            return pRepo.GetAll();
        }

        public Pinfo GetById(int id)
        {
            return pRepo.GetById(id);
        }

        public Pinfo delete(int id)
        {
            return pRepo.delete(id);
        }

        public Pinfo AddOrEdit(Pinfo emp)
        {
            if (emp.Id == 0)
            {
                return pRepo.add(emp);
            }
            else
            {
                return pRepo.edit(emp);
            }
        }
    }
}
