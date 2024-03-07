using LuftbornChallenge.DTO;
using LuftbornChallenge.Helpers;
using LuftbornChallenge.Models;
using LuftbornChallenge.Repositories;

namespace LuftbornChallenge.Services {
    public class EmployeeService {
        private readonly IRepository<Employee> _emplyeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IRepository<Employee> emplyeeRepository) {
            _logger = logger;
            _emplyeeRepository = emplyeeRepository;
        }

        public async Task<PagedResult<Employee>> GetPagedEmployees(int pageNumber, int pageSize) {
            return await _emplyeeRepository.GetAllPagedAsync(pageNumber, pageSize);
        }
        public async Task<Employee> GetEmployeeById(int employeeId) {
            return await _emplyeeRepository.GetByIdAsync(employeeId);
        }
        public async Task<Employee> CreateEmployee(EmployeeDto employeeDto) {
            Employee employee = new Employee() {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Dob = employeeDto.Dob,
                Education = employeeDto.Education,
                Email = employeeDto.Email,
                Experience = employeeDto.Experience,
                Gender = employeeDto.Gender
            };
            employee = await _emplyeeRepository.CreateAsync(employee);
            return employee;
        }
        public async Task UpdateEmployee(PutEmployeeDto employeeDto) {
            var oldEmployee = await _emplyeeRepository.GetByIdAsync(employeeDto.Id);
            if (oldEmployee is null) { throw new KeyNotFoundException(); }

            oldEmployee.FirstName = employeeDto.FirstName ?? oldEmployee.FirstName;
            oldEmployee.LastName = employeeDto.LastName ?? oldEmployee.LastName;
            oldEmployee.Email = employeeDto.Email ?? oldEmployee.Email;
            oldEmployee.Experience = employeeDto.Experience ?? oldEmployee.Experience;
            oldEmployee.Education = employeeDto.Education ?? oldEmployee.Education;
            oldEmployee.Dob = employeeDto.Dob ?? oldEmployee.Dob;
            oldEmployee.Gender = employeeDto.Gender ?? oldEmployee.Gender;

            await _emplyeeRepository.UpdateAsync(oldEmployee);
        }
        public async Task DeleteEmployee(int employeeId) {
            if (!await _emplyeeRepository.Exists(employeeId)) { throw new KeyNotFoundException(); }
            await _emplyeeRepository.DeleteAsync(employeeId);
        }
    }
}
