using System.Collections.Generic;

namespace CFL_1.CFL_Data.Cimetiere
{
    public class Cimetiere : Lieu
    {
        public string nom { get; set; }
        List<Sepulture> sepultures { get; set; } = new List<Sepulture>();
    }
}
