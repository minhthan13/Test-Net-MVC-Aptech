using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public class RequestServiceImpl : RequestService
  {
    private readonly TestnetmvcContext db;
    public RequestServiceImpl(TestnetmvcContext _db)
    {
      db = _db;
    }
    public List<Request> FindAll()
    {
      return db.Requests.ToList();
    }

    public Request FindById(int RequestId)
    {
      return db.Requests.Find(RequestId);
    }

    public List<Request> FindByPriorityId(int PriorityId)
    {
      return db.Requests.Where(r => r.PriorityId == PriorityId).ToList();
    }

    public List<Request> FindRequestEmployeeHandleId(int EmployeesId)
    {
      return db.Requests.Where(r => r.EmployeeIdHandling == EmployeesId).ToList();
    }

    public List<Request> FindRequestEmployeeSubmitId(int EmployeesId)
    {
      return db.Requests.Where(r => r.EmployeeIdSubmit == EmployeesId).ToList();
    }


    public bool AddNewRequest(Request request)
    {
      try
      {

        db.Requests.Add(request);
        return db.SaveChanges() > 0;

      }
      catch
      {
        return false;
      }


    }
  }
}