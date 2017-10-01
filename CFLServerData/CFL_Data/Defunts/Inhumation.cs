
using System.ComponentModel.DataAnnotations;
using CFL_1.CFL_Data.Cimetiere;

namespace CFL_1.CFL_Data.Defunts
{
    public enum Enveloppe
    {
        [Display(Description = "Cercueil")]
        Cercueil,
        [Display(Description = "Boite de réduction")]
        Boite_reduction,
        [Display(Description = "Urne")]
        Urne
    }

    /// <summary>
    /// Hérite de OperationFune.
    /// Contient <see cref="Sepulture"/> sepulture,
    /// <see cref="Enveloppe"/> enveloppe (cercueil, boitte de réduction, urne),
    /// bool scellementUrne.
    /// </summary>
    public class Inhumation : OperationFune
    {
        public Sepulture sepulture { get; set; }
        public Enveloppe enveloppe { get; set; }
        public bool scellementUrne { get; set; }
    }
}
