using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestNetMVC.Models;

public partial class TestnetmvcContext : DbContext
{
  public TestnetmvcContext()
  {
  }

  public TestnetmvcContext(DbContextOptions<TestnetmvcContext> options)
      : base(options)
  {
  }

  public virtual DbSet<Employee> Employees { get; set; }

  public virtual DbSet<Priority> Priorities { get; set; }

  public virtual DbSet<Request> Requests { get; set; }

  public virtual DbSet<Role> Roles { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Employee>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_nhanvien_1");
    });

    modelBuilder.Entity<Request>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Request__3214EC07B8A11A03");

      entity.HasOne(d => d.EmployeeIdHandlingNavigation).WithMany(p => p.RequestEmployeeIdHandlingNavigations).HasConstraintName("FK_Request_EmployeesHandling");

      entity.HasOne(d => d.EmployeeIdSubmitNavigation).WithMany(p => p.RequestEmployeeIdSubmitNavigations)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Request_EmployeesSubmit");

      entity.HasOne(d => d.Priority).WithMany(p => p.Requests)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Request_Priority");
    });

    modelBuilder.Entity<Role>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_role");

      entity.HasMany(d => d.Employees).WithMany(p => p.Roles)
              .UsingEntity<Dictionary<string, object>>(
                  "EmployeeRole",
                  r => r.HasOne<Employee>().WithMany()
                      .HasForeignKey("EmployeeId")
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EmployeeRole_Employees"),
                  l => l.HasOne<Role>().WithMany()
                      .HasForeignKey("RoleId")
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_EmployeeRole_Role"),
                  j =>
                  {
                  j.HasKey("RoleId", "EmployeeId").HasName("PK_EmployeeRole_RoleId");
                  j.ToTable("EmployeeRole");
                  j.HasIndex(new[] { "EmployeeId", "RoleId" }, "IX_EmployeeRole");
                  j.IndexerProperty<int>("RoleId").HasColumnName("roleId");
                  j.IndexerProperty<int>("EmployeeId").HasColumnName("employeeId");
                });
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
