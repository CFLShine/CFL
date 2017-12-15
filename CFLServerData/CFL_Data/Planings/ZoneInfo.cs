using System.Collections.Generic;
using MSTD.ShBase;

namespace CFL_1.CFL_Data
{
    public class ZoneInfo : Base
    {
        public Defunt defunt { get; set; }
        public List<ZoneAction> actions{ get; set; } = new List<ZoneAction>();
    }
}
