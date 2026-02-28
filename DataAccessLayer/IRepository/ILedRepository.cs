using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ILedRepository
    {
        public Task<int> GetIdDeviceByLineNameAsync(string lineName);
        public Task<string> GetDeviceNameByIdAsync(int id);
    }
}
