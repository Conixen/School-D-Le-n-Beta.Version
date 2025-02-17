using System;
using System.Collections.Generic;

namespace School_Dé_León_Beta.Version.Models;

public partial class Roll
{
    public int RollId { get; set; }

    public string RoleName { get; set; } = null!;

    public decimal Salary { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
