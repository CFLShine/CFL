
using System;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    public class ZoneAction : Base
    {
        public TimeSpan? horaire{ get; set; } = new TimeSpan();
        public string intitule{ get; set; } = "";
    }
}
