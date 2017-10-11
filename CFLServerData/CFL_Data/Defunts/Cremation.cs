
using System;
using CFL_1.CFL_Data.Communes;

namespace CFL_1.CFL_Data.Defunts
{
    public enum DestinationCendres
    {
        RemisesPf,
        InhumationSepulture,
        InhumationColumbarium,
        ScelleeMonument,
        DispersionJardinSouvenir,
        DispersionNature,
        DepotProvisoire
    }

    /// <summary>
    /// Hérite de <see cref="OperationFune"/>.
    /// Contient <see cref="DestinationCendres"/> destinationCendres
    /// et toutes les données utiles à la fiche crémation.
    /// La remise des cendres n'est pas représentée ici 
    /// mais par la classe <see cref="RemiseCendres"/> qui est une OperationFune
    /// et se retrouve donc dans <see cref="Defunt.OperationsFuneraires"/>.
    /// </summary>
    public class Cremation : OperationFune
    {
        public DestinationCendres destitationCendres { get; set; }

        public Utilisateur operateur { get; set; }

        public TimeSpan heureDebut { get; set; }
        public TimeSpan heureFin { get; set; }

        public Four four { get; set; }
        public int noOrdre { get; set; }
        public DateTime dateAutorisation { get; set; }
        public Commune autorisationDelivreeA { get; set; }

    }
}
