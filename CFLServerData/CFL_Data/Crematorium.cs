
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CFL_1.CFL_Data.Planings;
using MSTD;

namespace CFL_1.CFL_Data
{
    public class Crematorium : Base
    {
        public RaisonSociale raisonSociale { get; set; } = new RaisonSociale();
        public Coordonnees coordonnees { get; set; } = new Coordonnees();

        [Display(AutoGenerateField = false)]
        public PlaningJournalier planingJournalier { get; set; } = new PlaningJournalier();

        public List<Four> fours { get; set; } = new List<Four>();
    }
}
