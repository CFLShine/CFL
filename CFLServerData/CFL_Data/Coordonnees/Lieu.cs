using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Lieu represente tout lieu (contient Coordonnees et Contacts)
    /// </summary>
    public class Lieu : Base
    {
        public string Nom { get; set; }
        public Coordonnees Coordonnees { get; set; } = new Coordonnees();
        public Contacts Contacts { get; set; } = new Contacts();
    }
}
