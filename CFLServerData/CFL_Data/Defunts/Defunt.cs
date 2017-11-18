
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CFL_1.CFL_Data.Etat_civil;

namespace CFL_1.CFL_Data.Defunts
{
    public class Defunt : Personne
    {
        public Deces Deces { get; set; } = new Deces();

        public Naissance Naissance { get; set; } = new Naissance();

        public Filiation Filiation { get; set; } = new Filiation();

        [Display(Name = "Situation familiale")]
        public SituationFamiliale SituationFamiliale { get; set; } = new SituationFamiliale();
        
        public Pouvoir Pouvoir { get; set; } = new Pouvoir();

        public List<OperationFune> OperationsFuneraires { get; set; } = new List<OperationFune>();
    }
}
