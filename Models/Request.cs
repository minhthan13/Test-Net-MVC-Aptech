using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestNetMVC.Models;

[Table("Request")]
public partial class Request
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    public DateOnly SentDate { get; set; }

    public int PriorityId { get; set; }

    public int EmployeeIdSubmit { get; set; }

    public int EmployeeIdHandling { get; set; }

    [ForeignKey("EmployeeIdHandling")]
    [InverseProperty("RequestEmployeeIdHandlingNavigations")]
    public virtual Employee EmployeeIdHandlingNavigation { get; set; } = null!;

    [ForeignKey("EmployeeIdSubmit")]
    [InverseProperty("RequestEmployeeIdSubmitNavigations")]
    public virtual Employee EmployeeIdSubmitNavigation { get; set; } = null!;

    [ForeignKey("PriorityId")]
    [InverseProperty("Requests")]
    public virtual Priority Priority { get; set; } = null!;
}
