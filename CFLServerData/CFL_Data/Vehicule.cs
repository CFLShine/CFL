
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    public class Vehicule : Base
    {
        public string marque { get; set; }
        public string immatriculation { get; set; }

        /// <summary>
        /// ex : corbillard, ambulance, ...
        /// </summary>
        public string fonction { get; set; }
    }
}
