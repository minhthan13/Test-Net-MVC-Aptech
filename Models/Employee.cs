using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TestNetMVC.Models;

public partial class Employee
{
  [Key]
  public int Id { get; set; }

  [StringLength(250)]
  public string Username { get; set; } = null!;

  [StringLength(250)]
  public string Password { get; set; } = null!;

  [StringLength(250)]
  public string? Fullname { get; set; }

  [Column(TypeName = "datetime")]
  public DateTime Dob { get; set; }

  public bool Status { get; set; }

  [StringLength(250)]
  public string? Photo { get; set; }


  [InverseProperty("EmployeeIdHandlingNavigation")]

  public virtual ICollection<Request> RequestEmployeeIdHandlingNavigations { get; set; } = new List<Request>();


  [InverseProperty("EmployeeIdSubmitNavigation")]

  public virtual ICollection<Request> RequestEmployeeIdSubmitNavigations { get; set; } = new List<Request>();

  [JsonIgnore]
  [ForeignKey("EmployeeId")]
  [InverseProperty("Employees")]

  public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
