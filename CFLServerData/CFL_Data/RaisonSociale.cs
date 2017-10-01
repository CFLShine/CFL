using MSTD;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Contient string nom, string siret, string ape, string habilitation.
    /// </summary>
    public class RaisonSociale : Base
    {
        public string nom { get; set; } = "";
        public string siret { get; set; } = "";
        public string ape { get; set; } = "";
        public string habilitation { get; set; } = "";
    }
}
