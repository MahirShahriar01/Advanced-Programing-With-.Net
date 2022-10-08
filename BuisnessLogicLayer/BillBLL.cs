using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class BillBLL
    {
        BillRepo billRepo;
        public BillBLL()
        {
            billRepo = new BillRepo();
        }
        public List<Bill> GetAll()
        {
            return billRepo.GetAll();
        }

        public Bill GetById(int id)
        {
            return billRepo.GetById(id);
        }

        public Bill delete(int id)
        {
            return billRepo.delete(id);
        }

        public Bill AddOrEdit(Bill emp)
        {
            if (emp.ID == 0)
            {
                return billRepo.add(emp);
            }
            else
            {
                return billRepo.edit(emp);
            }
        }
    }
}
