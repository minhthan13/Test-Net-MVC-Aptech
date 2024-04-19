using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
      return db.Requests.OrderByDescending(r => r.SentDate).ToList();
    }

    public Request FindById(int RequestId)
    {
      return db.Requests.Find(RequestId);
    }



    public List<Request> FindRequestEmployeeHandleId(int EmployeesId)
    {
      return db.Requests.Where(r => r.EmployeeIdHandling == EmployeesId).OrderByDescending(r => r.SentDate).ToList();
    }

    public List<Request> FindRequestEmployeeSubmitId(int EmployeesId)
    {
      return db.Requests.Where(r => r.EmployeeIdSubmit == EmployeesId).OrderByDescending(r => r.EmployeeIdHandling).ToList();
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

    public bool ChangeHandleRequest(int ESuppId, int requestId)
    {
      var request = FindById(requestId);
      request.EmployeeIdHandling = ESuppId;

      try
      {
        db.Entry(request).State = EntityState.Modified;
        return db.SaveChanges() > 0;
      }
      catch (System.Exception)
      {
        return false;
      }


    }


    public List<Request> FindByPriorityId(int PriorityId)
    {
      if (PriorityId == -1)
      {
        return db.Requests.ToList();
      }
      else
      {
        return db.Requests.Where(r => r.PriorityId == PriorityId).ToList();
      }
    }
    public List<Request> FindByDates(string from, string to)
    {
      DateTime start = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      DateTime end = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      return db.Requests.Where(p => p.SentDate >= start && p.SentDate <= end).ToList();
    }
    public dynamic FilterRequest(string fromDate, string toDate, int priorityId)
    {
      // Khởi tạo một danh sách để lưu trữ kết quả lọc
      var resRequests = new List<Request>();

      // Kiểm tra nếu có ngày bắt đầu (fromDate) được cung cấp
      if (!string.IsNullOrEmpty(fromDate))
      {
        // Chuyển đổi fromDate thành đối tượng DateTime
        DateTime start = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        // Lọc các yêu cầu có ngày gửi lớn hơn hoặc bằng fromDate
        resRequests = db.Requests.Where(p => p.SentDate >= start).ToList();
      }

      // Kiểm tra nếu có ngày kết thúc (toDate) được cung cấp
      if (!string.IsNullOrEmpty(toDate))
      {
        // Chuyển đổi toDate thành đối tượng DateTime
        DateTime end = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        // Nếu đã có các yêu cầu được lọc từ trước, tiếp tục lọc trong phạm vi đó
        if (resRequests.Any())
        {
          resRequests = resRequests.Where(p => p.SentDate <= end).ToList();
        }
        else
        {
          // Nếu không có yêu cầu nào được lọc trước đó, lọc các yêu cầu có ngày gửi nhỏ hơn hoặc bằng toDate
          resRequests = db.Requests.Where(p => p.SentDate <= end).ToList();
        }
      }

      // Kiểm tra nếu có priorityId được cung cấp và nếu có yêu cầu đã được lọc trước đó
      if (priorityId != -1 && resRequests.Any())
      {
        // Lọc các yêu cầu trong phạm vi đã được lọc trước đó và có priorityId tương ứng
        resRequests = resRequests.Where(p => p.PriorityId == priorityId).ToList();
      }
      else if (priorityId != -1)
      {
        // Nếu không có yêu cầu nào được lọc trước đó, lọc tất cả các yêu cầu có priorityId tương ứng
        resRequests = db.Requests.Where(p => p.PriorityId == priorityId).ToList();
      }

      // Trả về danh sách các yêu cầu đã được lọc
      return resRequests.Select(r => new
      {
        title = r.Title,
        sentDate = r.SentDate.ToString("dd-MM-yyyy"),
        priorityName = r.Priority.PriorityName,
        description = r.Description,
        handler = r?.EmployeeIdHandlingNavigation?.Username ?? "none",
        sender = r.EmployeeIdSubmitNavigation.Username
      }).OrderBy(r => r.handler).ToList();

    }

    public dynamic GetRequestPaginate(int id, int skipItem, int pageSize)
    {
      var RoleUser = db.Employees.Find(id).Roles.FirstOrDefault().RoleName;
      var resRequests = new List<Request>();
      switch (RoleUser)
      {
        case "Admin":
          resRequests = FindAll().Skip(skipItem).Take(pageSize).ToList();
          break;
        case "Staff":
          resRequests = FindRequestEmployeeSubmitId(id).Skip(skipItem).Take(pageSize).ToList();
          break;
        case "Support Staff":
          resRequests = FindRequestEmployeeHandleId(id).Skip(skipItem).Take(pageSize).ToList();
          break;
        default:
          resRequests = FindAll();
          break;
      }
      return resRequests.Select(r => new
      {
        title = r.Title,
        sentDate = r.SentDate.ToString("dd-MM-yyyy"),
        priorityName = r.Priority.PriorityName,
        description = r.Description,
        handler = r?.EmployeeIdHandlingNavigation?.Username ?? "none",
        sender = r.EmployeeIdSubmitNavigation.Username
      }).OrderBy(r => r.handler).ToList();
    }

    public int GetTotalRequestCount(int userId)
    {
      var RoleUser = db.Employees.Find(userId).Roles.FirstOrDefault().RoleName;
      var requestCount = 0;
      switch (RoleUser)
      {
        case "Admin":
          requestCount = FindAll().Count();
          break;
        case "Staff":
          requestCount = FindRequestEmployeeSubmitId(userId).Count();
          break;
        case "Support Staff":
          requestCount = FindRequestEmployeeHandleId(userId).Count();
          break;
        default:
          requestCount = FindAll().Count(); ;
          break;
      }
      return requestCount;
    }
  }
}