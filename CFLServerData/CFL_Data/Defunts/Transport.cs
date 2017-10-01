using System;
using CFL_1.CFL_Data.Defunts;

namespace CFL_1.CFL_Data.Defunts
{
    /// <summary>
    /// Hérite d' <see cref="OperationFune"/>.
    /// Contient <see cref="Lieu"/> lieuArrivee,
    /// DateTime? dateArrivee, TimeSpan? heureArrivee, <see cref="Vehicule"/> vehicule.
    /// Les dates, heure et lieu de départ sont représentés
    /// par <see cref="OperationFune.date"/>, <see cref="OperationFune.heure"/> et <see cref="OperationFune.lieu"/>.
    /// </summary>
    public class Transport : OperationFune
    {
        Lieu lieuArrivee { get; set; }
        DateTime? dateArrivee { get; set; }
        TimeSpan? heureArrivee { get; set; }
        public Vehicule vehicule { get; set; }
    }
}
