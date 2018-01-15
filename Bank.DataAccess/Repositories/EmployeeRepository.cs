using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;
using System.Data.Entity;

namespace Bank.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private BankContext _context;

        public EmployeeRepository(BankContext context)
        {
            _context = context;
        }

        public void addNewEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void deleteEmployee(int id)
        {
            getEmployeeById(id).IsSuspended = true;
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

        public List<Employee> getEmployeeList()
        {
            return _context.Employees.Where(e => !e.IsSuspended).OrderBy(e => e.LastName).ToList();
        }

        public void updateEmployee(Employee employee)
        {
            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
