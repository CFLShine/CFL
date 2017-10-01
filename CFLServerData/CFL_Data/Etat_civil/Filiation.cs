using System.ComponentModel.DataAnnotations;
using MSTD;

namespace CFL_1.CFL_Data.Etat_civil
{
    public class Filiation : Base
    {
        [Display(Name = "Père")]
        public Parent pere { get; set; } = new Parent();
        [Display(Name = "Mère")]
        public Parent mere { get; set; } = new Parent();
    }
}
