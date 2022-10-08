using DataAccessLayer;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer
{
    public class AddressBLL
    {
        AddressRepo addressRepo;
        public AddressBLL()
        {
            addressRepo = new AddressRepo();
        }
        public List<Address> GetAll()
        {
            return addressRepo.GetAll();
        }

        public Address GetById(int id)
        {
            return addressRepo.GetById(id);
        }

        public Address delete(int id)
        {
            return addressRepo.delete(id);
        }

        public Address AddOrEdit(Address emp)
        {
            if (emp.Id == 0)
            {
                return addressRepo.add(emp);
            }
            else
            {
                return addressRepo.edit(emp);
            }
        }
    }
}
