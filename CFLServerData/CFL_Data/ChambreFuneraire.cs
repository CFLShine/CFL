
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MSTD;

namespace CFL_1.CFL_Data
{
    /// <summary>
    /// Hérite de Base
    /// </summary>
    public class ChambreFuneraire : Base
    {
        public RaisonSociale raisonSociale { get; set; } = new RaisonSociale();
        public Coordonnees coordonnees { get; set; } = new Coordonnees();
        public Contacts contacts { get; set; } = new Contacts();

        [Display(AutoGenerateField = false)]
        public List<Salon> salons { get; set; } = new List<Salon>();
        [Display(AutoGenerateField = false)]
        public List<CaseRefrigeree> cases { get; set; } = new List<CaseRefrigeree>();
    }
}
