
using Dapper;
using HealthCare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HT.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var employees = db.Query<Employee>("SELECT * FROM Employee");

            foreach (var employee in employees)
            {
                CalculateValues(employee);
            }

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            string query = @"INSERT INTO Employee (EmployeeCode, EmployeeName, DateOfBirth, Gender, Department, Designation, BasicSalary)
                             VALUES (@EmployeeCode, @EmployeeName, @DateOfBirth, @Gender, @Department, @Designation, @BasicSalary)";

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Execute(query, employee);
            }

            return RedirectToAction("Index");
        }

        private void CalculateValues(Employee employee)
        {
            employee.DearnessAllowance = employee.BasicSalary * 0.4;
            employee.ConveyanceAllowance = Math.Min(employee.DearnessAllowance * 0.1, 250);
            employee.HouseRentAllowance = Math.Max(employee.BasicSalary * 0.25, 1500);
            employee.GrossSalary = employee.BasicSalary + employee.DearnessAllowance + employee.ConveyanceAllowance + employee.HouseRentAllowance;

            if (employee.GrossSalary <= 3000)
            {
                employee.PT = 100;
            }
            else if (employee.GrossSalary <= 6000)
            {
                employee.PT = 150;
            }
            else
            {
                employee.PT = 200;
            }

            employee.TotalSalary = employee.BasicSalary + employee.DearnessAllowance + employee.ConveyanceAllowance + employee.HouseRentAllowance - employee.PT;
        }

       
    }
}
