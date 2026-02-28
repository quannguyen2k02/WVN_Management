using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LED> LEDs { get; set; }
        public DbSet<LedModel> LEDModels { get; set; }
        public DbSet<LedModelConfig> ledModelConfigs { get; set; }
        public DbSet<LedCamera> LedCameras { get; set; }
        public DbSet<LedStatus> LedStatuses { get; set; }
        public DbSet<Job> Jobs { get; set; }

    }
}
