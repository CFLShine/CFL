using System;
using CFL_1.CFL_Data.Communes;
using MSTD.ShBase;

namespace CFL_1.CFL_Data.Etat_civil
{
    public enum Situation
    {
        Celibataire,
        Marie,
        Veuf,
        Divorce,
        Pacse
    }

    public class SituationFamiliale : Base
    {
        public Situation situation { get; set; }
        public Identite identite { get; set; }
        DateTime? dateNaissance { get; set; }
        DateTime? dateMariage { get; set; }
        DateTime? dateDivorce { get; set; }
        DateTime? dateDece { get; set; }
        Commune communeDeces { get; set; }
        public int nombreEnfants { get; set; } = 0;
        public int enfantsVivants { get; set; } = 0;
    }
}
