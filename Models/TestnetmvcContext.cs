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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =KRANTZ\\SQLEXPRESS;Initial Catalog=testnetmvc;User ID =SA;Password=vtdtedvta1001;TrustServerCertificate = True");

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
