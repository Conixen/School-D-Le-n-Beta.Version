using System;
using System.Collections.Generic;

namespace School_Dé_León_Beta.Version.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int EmployeId { get; set; }

    public virtual Employe Employe { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
