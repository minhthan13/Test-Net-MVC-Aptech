using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
  }
}