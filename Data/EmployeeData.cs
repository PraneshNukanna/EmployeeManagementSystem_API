using API_ManagementSystem_ClassActivity.Models;
using Microsoft.EntityFrameworkCore;

namespace API_ManagementSystem_ClassActivity.Data
{
    public class EmployeeData
    {
        private readonly ApiDbContext _context;

        public EmployeeData(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAll()
        {
            //return await _context.Employees
            //    .OrderBy(x => x.FirstName)
            //    .ThenBy(x => x.LastName)
            //    .ToListAsync();
            return await _context.Employees
                .Include(x => x.Title)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync();
        }

        public async Task<Employee> Get(Guid id)
        {
            //return await _context.Employees.FindAsync(id);
            return await _context.Employees
                .Include(x => x.Title)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Employee>> Search(string keyword)
        {
            return await _context.Employees
                .Include(x => x.Title)
                .Where(x => x.FirstName.Contains(keyword) || x.LastName.Contains(keyword))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync();
            //return await _context.Employees
            //    .Where(x => x.FirstName.Contains(keyword) || x.LastName.Contains(keyword))
            //    .OrderBy(x => x.FirstName)
            //    .ThenBy(x => x.LastName)
            //    .ToListAsync();
        }

        public async Task<Employee> Create(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task Delete(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
