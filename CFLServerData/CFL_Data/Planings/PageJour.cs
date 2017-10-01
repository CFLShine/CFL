
using System;
using System.Collections.Generic;
using CFL_1.CFL_System;
using MSTD;

namespace CFL_1.CFL_Data.Planings
{
    public class PageJour : Base
    {
        public PlaningJournalier planing { get; set; }
        public DateTime? Day { get; set; }
        public List<ZoneInfo> zones { get; set; } = new List<ZoneInfo>();
    }
}
