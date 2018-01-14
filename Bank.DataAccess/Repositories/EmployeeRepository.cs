using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;

namespace Bank.DataAccess.Repositories
{
    class EmployeeRepository : IEmployeeRepository
    {
        private BankContext _context;

        public EmployeeRepository(BankContext context)
        {
            _context = context;
        }

        public void addNewEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
        }

        public Employee getEmployeeByEmail(string email)
        {
            return _context.Employees.Where(e => e.Email == email).FirstOrDefault();
        }

        public Employee getEmployeeById(int id)
        {
            return _context.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public IEnumerable getEmployeeList()
        {
            return _context.Employees.OrderBy(e => e.LastName).ToList();
        }

        public void updateEmployee(Employee employee)
        {
            throw new NotImplementedException();
            // TODO
        }
    }
}
