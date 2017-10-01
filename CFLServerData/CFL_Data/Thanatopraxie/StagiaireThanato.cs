
using System.Collections.Generic;
using CFL_1.CFL_Data.Defunts;

namespace CFL_1.CFL_Data.Thanatopraxie
{
    /// <summary>
    /// Hérite de <see cref="Personne"/>.
    /// Contient int annee,
    /// <see cref="DBList{T}"/> (de  Soin) soins.
    /// </summary>
    public class StagiaireThanato : Personne
    {
        public int annee { get; set; }
        public List<Soin> soins { get; set; }
    }
}
