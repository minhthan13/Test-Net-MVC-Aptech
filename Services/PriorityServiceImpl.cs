using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public class PriorityServiceImpl : PriorityService
  {
    private readonly TestnetmvcContext db;
    public PriorityServiceImpl(TestnetmvcContext _db)
    {
      db = _db;
    }
    public List<Priority> FindAll()
    {
      return db.Priorities.ToList();
    }

    public Priority FindById(int priorityId)
    {
      return db.Priorities.Find(priorityId);
    }
  }
}