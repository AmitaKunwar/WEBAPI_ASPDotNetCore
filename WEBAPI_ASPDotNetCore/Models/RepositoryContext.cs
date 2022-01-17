using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_ASPDotNetCore.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; } // EMPLOYEE is the table name in SchoolDB database
        public DbSet<LoadImage> TblHOMEPAGES { get; set; }       

       
    }
}
