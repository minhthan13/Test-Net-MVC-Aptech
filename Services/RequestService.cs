using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNetMVC.Models;

namespace TestNetMVC.Services
{
  public interface RequestService
  {
    public List<Request> FindAll();
    public Request FindById(int RequestId);
    public List<Request> FindByPriorityId(int PriorityId);
    public List<Request> FindRequestEmployeeHandleId(int EmployeesId);
    public List<Request> FindRequestEmployeeSubmitId(int EmployeesId);

    public bool AddNewRequest(Request request);

  }
}