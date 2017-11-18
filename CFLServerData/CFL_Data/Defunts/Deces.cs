using System;
using MSTD.ShBase;

namespace CFL_1.CFL_Data.Defunts
{
    /// <summary>
    /// date, heure, Lieu, medecin
    /// </summary>
    public class Deces : Base
    {
        public DateTime? date { get; set; }
        public TimeSpan? heure { get; set; }
        public Lieu lieu { get; set; } = new Lieu();
        public string medecin { get; set; } = "";
    }
}
