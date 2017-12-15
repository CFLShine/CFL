
using System;
using System.Collections.Generic;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    public class PageJour : Base
    {
        public PlaningJournalier planing { get; set; }
        public DateTime? Day { get; set; }
        public List<ZoneInfo> zones { get; set; } = new List<ZoneInfo>();
    }
}
