using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuftbornChallenge.Models;
using LuftbornChallenge.Services;
using LuftbornChallenge.Helpers;
using LuftbornChallenge.DTO;

namespace LuftbornChallenge.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase {
        private readonly EmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(EmployeeService employeeService, ILogger<EmployeesController> logger) {
            _employeeService = employeeService;
            _logger = logger;
        }



        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<PagedResult<Employee>>> GetEmployees(int pageNumber = 1, int pageSize = 10) {
            _logger.LogInformation("get all employees page {pageNumber}", pageNumber);
            return await _employeeService.GetPagedEmployees(pageNumber, pageSize);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id) {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null) {
                _logger.LogWarning("Employee {employeeId} Not Found", id);
                return NotFound();
            }
            _logger.LogInformation("get Employee {employeeId}", id);
            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, PutEmployeeDto employeeDto) {
            if (id != employeeDto.Id) {
                _logger.LogWarning("PUT id mismatch");
                return BadRequest();
            }



            try {
                await _employeeService.UpdateEmployee(employeeDto);
            } catch (KeyNotFoundException) {
                _logger.LogWarning("Employee not found {id}", id);
                return NotFound();
            }
            _logger.LogInformation("Updated Employee {Id}", employeeDto.Id);
            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employeeDto) {
            var employee = await _employeeService.CreateEmployee(employeeDto);
            _logger.LogInformation("Created an employee");

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id) {
            try {
                await _employeeService.DeleteEmployee(id);
            } catch (KeyNotFoundException) {
                _logger.LogWarning("Employee not found {id}", id);
                return NotFound();
            }

            _logger.LogInformation("Deleted Employee {Id}", id);

            return NoContent();
        }

    }
}