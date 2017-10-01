
using CFL_1.CFL_Data.Etat_civil;

namespace CFL_1.CFL_Data.Defunts
{
    /// <summary>
    /// Hérite de <see cref="OperationFune"/>.
    /// Contient <see cref="Pouvoir"/> remisesA.
    /// </summary>
    public class RemiseCendres : OperationFune
    {
        Pouvoir remisesA { get; set; }
    }
}
