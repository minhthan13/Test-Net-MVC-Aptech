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

    public List<Role> ListRole()
    {
      return db.Roles.ToList();
    }

    public string GetRoleNamesByEmployeeId(int employeeId)
    {
      var roleName = db.Employees
            .Where(e => e.Id == employeeId)
            .Select(e => e.Roles.FirstOrDefault().RoleName)
            .FirstOrDefault();

      return roleName;
    }

    public List<Employee> FindAll()
    {
      return db.Employees.ToList();
    }

    public bool AddNewEmployee(Employee employee, int RoleId)
    {
      try
      {
        // Thêm Employee vào DbContext
        db.Employees.Add(employee);
        db.SaveChanges();
        var newEmpl = FindByUsername(employee.Username);

        db.Database.ExecuteSqlRaw(
            "INSERT INTO EmployeeRole (employeeId, roleId) VALUES ({0}, {1})",
            newEmpl.Id, RoleId);

        return db.SaveChanges() > 0;

      }
      catch
      {
        return false;
      }


    }
  }
}