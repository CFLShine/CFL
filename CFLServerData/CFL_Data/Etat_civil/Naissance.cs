using System;
using MSTD.ShBase;

namespace CFL_1.CFL_Data.Etat_civil
{
    /// <summary>
    /// Coordonnees + date
    /// </summary>
    public class Naissance : Base
    {
        public DateTime? date { get; set; }
        public Coordonnees lieu { get; set; } = new Coordonnees();
    }
}
