using CFL_1.CFL_Data.Communes;
using MSTD;

namespace CFL_1.CFL_Data
{
    public class Coordonnees : Base
    {
        public Coordonnees() { }

        public string adress1 { get ; set ; } = "";
        
        public string adress2  { get ; set ; } = "";

        public Commune   commune { get ; set ; } = null;

        public string pays { get ; set ; } = "";

    }
}
