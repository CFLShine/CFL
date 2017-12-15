using System;
using System.ComponentModel.DataAnnotations;

namespace CFL_1.CFL_Data
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
        [Display(Name = "Lieu d'arrivée")]
        Lieu lieuArrivee { get; set; }

        [Display(Name = "Date d'arrivée")]
        DateTime? dateArrivee { get; set; }

        [Display(Name = "Heure d'arrivée")]
        TimeSpan? heureArrivee { get; set; }

        [Display(Name = "Véhicule")]
        public Vehicule vehicule { get; set; }

        [Display(Name = "Avant mise en bière")]
        public bool TAMB { get; set; }
    }
}
