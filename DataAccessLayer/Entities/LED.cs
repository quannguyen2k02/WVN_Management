using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class LED
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int LineId { get; set; }
        public ICollection<LedModel> LedModels { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
