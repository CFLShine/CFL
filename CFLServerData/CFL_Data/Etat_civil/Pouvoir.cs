using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFL_1.CFL_System;

namespace CFL_1.CFL_Data.Etat_civil
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
    /// Contient <see cref="Qualite"/> qualite.
    /// </summary>
    public class Pouvoir : Personne
    {
        public Qualite qualite { get; set; }
    }
}
