using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace School_Dé_León_Beta.Version.Models;

public partial class SchoolDèLéonApplikationenContext : DbContext
{
    public SchoolDèLéonApplikationenContext()
    {

    }

    public SchoolDèLéonApplikationenContext(DbContextOptions<SchoolDèLéonApplikationenContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Roll> Rolls { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = localhost; Database=SchoolDèLéonApplikationen;Integrated Security = True; Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927A01EE8DFB6");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.EmployeId).HasColumnName("EmployeID");

            entity.HasOne(d => d.Employe).WithMany(p => p.Classes)
                .HasForeignKey(d => d.EmployeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class__EmployeID__3F466844");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD25FC19FA");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.EmployeId).HasName("PK__Employe__6251440F52D0B5F2");

            entity.ToTable("Employe");

            entity.Property(e => e.EmployeId).HasColumnName("EmployeID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmployeFname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EmployeFName");
            entity.Property(e => e.EmployeLname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EmployeLName");
            entity.Property(e => e.RollId).HasColumnName("RollID");

            entity.HasOne(d => d.Department).WithMany(p => p.Employes)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employe__Departm__3C69FB99");

            entity.HasOne(d => d.Roll).WithMany(p => p.Employes)
                .HasForeignKey(d => d.RollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employe__RollID__3B75D760");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grade__54F87A3766F30E81");

            entity.ToTable("Grade");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.GradeValue).HasColumnName("GradeValue");
            entity.Property(e => e.DateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EmployeId).HasColumnName("EmployeID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Employe).WithMany(p => p.Grades)
                .HasForeignKey(d => d.EmployeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grade__EmployeID__4BAC3F29");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grade__StudentID__4AB81AF0");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Grade__SubjectID__4CA06362");
        });

        modelBuilder.Entity<Roll>(entity =>
        {
            entity.HasKey(e => e.RollId).HasName("PK__Roll__7886EE3F2F7D0767");

            entity.ToTable("Roll");

            entity.Property(e => e.RollId).HasColumnName("RollID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A79E9E36AC9");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Ssnum, "UQ__Student__51CD19FBDFE67FBB").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Ssnum)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SSNUM");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student__ClassID__4316F928");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__AC1BA38890EB9636");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
