using MSTD;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Contient <see cref="RaisonSociale"/>, <see cref="Coordonnees"/>, <see cref="Contacts"/>.
    /// </summary>
    public class Pf : Base
    {
        public RaisonSociale raisonSociale { get; set; } = new RaisonSociale();
        public Coordonnees coordonnees { get; set; } = new Coordonnees();
        public Contacts contacts { get; set; }
    }
}
