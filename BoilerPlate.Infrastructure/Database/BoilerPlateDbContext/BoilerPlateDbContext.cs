using BoilerPlate.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Infrastructure.Database.BoilerPlateDbContext
{
    public class BoilerPlateDbContext : DbContext
    {
        public BoilerPlateDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> UsersEntity { get; set; }
          
    };
    
    
}
