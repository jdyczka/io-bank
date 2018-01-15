using Bank.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> getEmployeeList();
        Employee getEmployeeById(int id);
        Employee getEmployeeByEmail(string email);
        void addNewEmployee(Employee employee);
        void updateEmployee(Employee employee);
    }
}
