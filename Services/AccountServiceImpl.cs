using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public class AccountServiceImpl : AccountService
  {
    private readonly TestnetmvcContext db;
    public AccountServiceImpl(TestnetmvcContext _db)
    {
      db = _db;

    }

    public bool Login(string username, string password)
    {
      var account = db.Employees.SingleOrDefault(e => e.Username == username && e.Status);
      if (account != null)
      {
        return BCrypt.Net.BCrypt.Verify(password, account.Password);
      }
      return false;
    }


    public Employee FindByUsername(string username)
    {
      return db.Employees.SingleOrDefault(e => e.Username == username);
    }


    public List<Employee> FindAll()
    {
      return db.Employees.ToList();
    }

    public bool AddNewEmployee(Employee employee)
    {
      try
      {

        db.Employees.Add(employee);
        return db.SaveChanges() > 0;

      }
      catch
      {
        return false;
      }
    }

    public List<Employee> FindEmployeeSupport()
    {
      return db.Employees.Where(e => e.Roles.Any(r => r.RoleName.Contains("Support Staff"))).ToList();
    }


  }
}