

namespace CFL_1.CFL_Data.Defunts
{
    public enum TypeSoin
    {
        Normal,
        Départ_etranger,
        Reconstruction,
        Corps_autopsié,
        Sanisation
    }

    public class Soin : OperationFune
    {
        Utilisateur thanatopracteur { get; set; }
        TypeSoin type_soin { get; set; }
    }
}
