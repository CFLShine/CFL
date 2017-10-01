using MSTD;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Hérite de Base.
    /// Contient bool lecture, bool sauvegarde, 
    /// Base referedClass (la classe à laquelle l'autorisation fait référence).
    /// </summary>
    public class Autorisation : Base
    {
        public bool lecture { get ; set ; } = false;
        public bool sauvegarde { get ; set ; } = false;
        
        // la classe à laquelle l'autorisation fait référence.
        public Base referedClass { get ; set ; }
    }
}
