using System;
using System.Collections.Generic;

namespace School_Dé_León_Beta.Version.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? GradeValue { get; set; }

    public DateOnly DateTime { get; set; }

    public int StudentId { get; set; }

    public int EmployeId { get; set; }

    public int SubjectId { get; set; }

    public virtual Employe Employe { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
