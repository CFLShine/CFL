using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Lieu represente tout lieu (contient Coordonnees et Contacts)
    /// </summary>
    public class Lieu : Base
    {
        public Coordonnees adresse { get; set; } = new Coordonnees();
        public Contacts contacts { get; set; } = new Contacts();
    }
}
