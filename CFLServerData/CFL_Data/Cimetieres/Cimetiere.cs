using System.Collections.Generic;

namespace CFL_1.CFL_Data
{
    public class Cimetiere : Lieu
    {
        List<Sepulture> sepultures { get; set; } = new List<Sepulture>();
    }
}
