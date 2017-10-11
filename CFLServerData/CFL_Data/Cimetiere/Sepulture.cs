
using System.Collections.Generic;
using CFL_1.CFL_Data.Defunts;
using MSTD.ShBase;

namespace CFL_1.CFL_Data.Cimetiere
{
    public enum TypeSepulture
    {
        PleineTerre,
        CaveauOuvertureDessus,
        CaveauOuvertureDevant,
    }

    public enum TypeMonument
    {
        Aucun,
        Pierre,
        Resine
    }

    public class Sepulture : Base
    {
        public Cimetiere cimetiere { get; set; }

        public bool carreEnfants { get; set; }
        public bool carreGeneral { get; set; }
        public bool jardinSouvernir { get; set; }

        public string Division { get; set; }
        public string Carre { get; set; }
        public string Ligne{ get; set; }
        public string Tombe{ get; set; }

        public TypeSepulture type { get; set; }

        public TypeMonument monument{ get; set; }

        public int places { get; set; }
        public List<Inhumation> inhumation { get; set; } = new List<Inhumation>();
    }
}
