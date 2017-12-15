

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Hérite de <see cref="OperationFune"/>.
    /// Contient string type, bool presse, bool registre, bool bourseDons.
    /// </summary>
    public class Ceremonie : OperationFune
    {
        public string type { get; set; }
        public bool presse { get; set; }
        public bool registre { get; set; }
        public bool bourseDons { get; set; }
    }
}
