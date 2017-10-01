
using System.Collections.Generic;
using CFL_1.CFL_Data.Etat_civil;

namespace CFL_1.CFL_Data.Defunts
{
    public class Defunt : Personne
    {
        public Deces deces { get; set; } = new Deces();
        public Naissance naissance { get; set; } = new Naissance();
        public Filiation filiation { get; set; } = new Filiation();
        public SituationFamillale situation { get; set; } = new SituationFamillale();
        public Pouvoir pouvoir { get; set; } = new Pouvoir();

        public List<OperationFune> operationsFuneraires { get; set; } = new List<OperationFune>();
    }
}
