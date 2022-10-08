using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class LiveBLL
    {
        LiveRepo liveRepo;
        public LiveBLL()
        {
            liveRepo = new LiveRepo();
        }
        public List<Live> GetAll()
        {
            return liveRepo.GetAll();
        }

        public Live GetById(int id)
        {
            return liveRepo.GetById(id);
        }

        public Live delete(int id)
        {
            return liveRepo.delete(id);
        }

        public Live AddOrEdit(Live emp)
        {
            if (emp.ID == 0)
            {
                return liveRepo.add(emp);
            }
            else
            {
                return liveRepo.edit(emp);
            }
        }
    }
}
