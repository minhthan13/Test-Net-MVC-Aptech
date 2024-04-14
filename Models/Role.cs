using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TestNetMVC.Models;

[Table("Role")]
public partial class Role
{
  [Key]
  public int Id { get; set; }

  [StringLength(250)]
  public string RoleName { get; set; } = null!;

  [ForeignKey("RoleId")]
  [InverseProperty("Roles")]
  [JsonIgnore]
  public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
