using MSTD;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Contient Identite, Coordonnees, Contacts.
    /// </summary>
    public class Personne : Base
    { 
        public Identite identite { get ; set ; } = new Identite();
        
        public Coordonnees coordonnees { get ; set ; } = new Coordonnees();

        public Contacts contacts { get ; set ; } = new Contacts() ;
    }
}
