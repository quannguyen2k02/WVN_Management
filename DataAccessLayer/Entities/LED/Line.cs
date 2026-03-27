using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities.LED
{
    [Table("Line")]
    public class Line
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<LED>? Leds { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
