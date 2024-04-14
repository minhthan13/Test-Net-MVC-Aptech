using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public interface RoleService
  {
    public List<Role> FindAll();
    public Role FindById(int RoleId);
  }
}