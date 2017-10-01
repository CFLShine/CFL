using System;
using System.Collections.Generic;
using MSTD;

namespace CFL_1.CFL_Data.Defunts
{
    /// <summary>
    /// Contient Defunt defunt, Date? date, TimeSpan? heure, <see cref="Lieu"/> lieu,
    /// <see cref="Pf"/> pf, string commentaire,  <see cref="DBList{T}"/> (de <see cref="Utilisateur"/> ) agents, <see cref="Utilisateur"/> ordonnateur.
    /// </summary>
    public abstract class OperationFune : Base
    {
        public Defunt defunt { get; set; }

        public DateTime? date { get; set; }
        public TimeSpan? heure { get; set; }
        public Lieu lieu { get; set; }
        public Pf pf { get; set; }
        public string commentaire { get; set; }

        /// <summary>
        /// équipe qui réalise l'opération.
        /// </summary>
        public List<Utilisateur> agents { get; set; } = new List<Utilisateur>();
        public Utilisateur ordonnateur { get; set; }
    }
}
