using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public interface AccountService
  {
    public bool Login(string username, string password);
    public Employee FindByUsername(string username);
    public List<Employee> FindAll();
    public dynamic FindAll2();

    public bool AddNewEmployee(Employee employee);
    public List<Employee> FindEmployeeSupport();

    public bool ActiveAcount(string username, bool isChecked);

    public bool EditAccount(Employee employee);

    public bool Exist(string username);

  }
}