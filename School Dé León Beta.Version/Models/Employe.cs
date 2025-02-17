using System;
using System.Collections.Generic;

namespace School_Dé_León_Beta.Version.Models;

public partial class Employe
{
    public int EmployeId { get; set; }

    public string EmployeFname { get; set; } = null!;

    public string EmployeLname { get; set; } = null!;

    public int RollId { get; set; }

    public int? DepartmentId { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Roll Roll { get; set; } = null!;
}
