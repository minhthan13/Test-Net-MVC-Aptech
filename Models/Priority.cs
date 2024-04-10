using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNetMVC.Models;

[Table("Priority")]
public partial class Priority
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string PriorityName { get; set; } = null!;

    [InverseProperty("Priority")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
