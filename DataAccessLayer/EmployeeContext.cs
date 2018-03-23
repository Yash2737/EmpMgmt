using System.Data.Entity;
using EmployeeManagement.Models;
using Helper;

namespace DataAccessLayer
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BankInfo> BankInfo { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
