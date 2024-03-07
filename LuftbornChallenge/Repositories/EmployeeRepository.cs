using LuftbornChallenge.Helpers;
using LuftbornChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace LuftbornChallenge.Repositories {
    public class EmployeeRepository : IRepository<Employee> {

        private readonly LuftbornContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ILogger<EmployeeRepository> logger, LuftbornContext context) {
            _logger = logger;
            _context = context;
        }

        public async Task<Employee> CreateAsync(Employee employee) {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteAsync(int id) {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null) {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id) {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<PagedResult<Employee>> GetAllPagedAsync(int pageNumber, int pageSize) {
            return await PagedResult<Employee>.CreateAsync(_context.Employees.AsNoTracking(), pageNumber, pageSize);
        }

        public async Task<Employee> GetByIdAsync(int id) {
            return await _context.Employees.FindAsync(id);
        }

        public async Task UpdateAsync(Employee employee) {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
