using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class ProfileBLL
    {
        ProfileRepo profileRepo;
        public ProfileBLL()
        {
            profileRepo = new ProfileRepo();
        }
        public List<Profile> GetAll()
        {
            return profileRepo.GetAll();
        }

        public Profile GetById(int id)
        {
            return profileRepo.GetById(id);
        }

        public Profile delete(int id)
        {
            return profileRepo.delete(id);
        }

        public Profile AddOrEdit(Profile emp)
        {
            if (emp.ID == 0)
            {
                return profileRepo.add(emp);
            }
            else
            {
                return profileRepo.edit(emp);
            }
        }
    }
}
