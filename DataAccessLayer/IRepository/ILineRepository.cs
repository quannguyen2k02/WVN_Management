using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ILineRepository
    {

        public Task<int?> GetIdByLineName(string name);
        public Task<string?> GetLineNameById(int id);
    }
}
