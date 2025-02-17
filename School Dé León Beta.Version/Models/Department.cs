using System;
using System.Collections.Generic;

namespace School_Dé_León_Beta.Version.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
