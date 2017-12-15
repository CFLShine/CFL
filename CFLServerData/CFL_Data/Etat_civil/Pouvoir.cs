
using System.ComponentModel.DataAnnotations;

namespace CFL_1.CFL_Data
{
    public enum Qualite
    {
        Conjoint,
        Pere,
        Mere,
        Fils,
        Fille,
        Frere,
        Soeur,
        Autre
    }

    /// <summary>
    /// Hérite de <see cref="Personne"/>.
    /// Contient <see cref="CFL_Data.Qualite"/> qualite.
    /// </summary>
    public class Pouvoir : Personne
    {
        public Qualite Qualite 
        { 
            get; 
            set; 
        }

        [DisplayAttribute(Name = "Qualite autre")]
        public string QualiteAutre
        {
            get;
            set;
        }
    }
}
