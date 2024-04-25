using API_ManagementSystem_ClassActivity.Models;
using Microsoft.EntityFrameworkCore;


namespace API_ManagementSystem_ClassActivity.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Title> Titles { get; set; }
    }
}
