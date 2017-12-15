using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    public class Entreprise : Base
    {
        public RaisonSociale raisonSociale { get; set; } = new RaisonSociale();
        public bool isOurEntreprise { get ; set ; } = false;
        public Coordonnees coordonnees { get ; set ; } = new Coordonnees();
        public Contacts contacts { get ; set ; } = new Contacts();
    }
}
