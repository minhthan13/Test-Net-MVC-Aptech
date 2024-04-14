using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public interface PriorityService
  {
    public List<Priority> FindAll();
    public Priority FindById(int priorityId);

  }
}