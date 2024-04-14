using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public class RoleServiceImpl : RoleService
  {
    private readonly TestnetmvcContext db;
    public RoleServiceImpl(TestnetmvcContext _db)
    {
      db = _db;
    }
    public List<Role> FindAll()
    {
      return db.Roles.ToList();
    }

    public Role FindById(int RoleId)
    {
      return db.Roles.Find(RoleId);
    }
  }
}