using MSTD;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Nom, nom jf, prenom, ..., nationalite, profession
    /// </summary>
    public class Identite : Base
    {
        public Identite() { }

        public string titre { get ; set ; } = "";

        public string nom { get ; set ; } = "";

        public string nomJf { get ; set ; } = "";
        
        public string prenom { get ; set ; } = "";

        public string prenoms { get ; set ; } = "";
        
        public string nationalite { get ; set ; } = "";
        
        public string profession { get ; set ; } = "";
        
    }
}
