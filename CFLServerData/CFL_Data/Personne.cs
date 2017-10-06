using System.ComponentModel.DataAnnotations;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Contient Identite, Coordonnees, Contacts.
    /// </summary>
    public class Personne : Base
    { 
        [Display(Name = "Identité")]
        public Identite Identite { get ; set ; } = new Identite();
        
        [Display(Name = "Coordonées")]
        public Coordonnees Coordonnees { get ; set ; } = new Coordonnees();

        public Contacts Contacts { get ; set ; } = new Contacts() ;
    }
}
