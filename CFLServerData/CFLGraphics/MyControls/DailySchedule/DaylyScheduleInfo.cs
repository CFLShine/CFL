using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SqlOrm;

namespace DailySchedule
{
    public class DaylyScheduleInfo
    {
        public DaylyScheduleInfo(DBContext _dbcontext)
        {
            DBContext = _dbcontext;
        }

        public ZonePatern ZonePatern { get; set; } = null;
        public List<SheetInfo> Sheets { get; set; } = new List<SheetInfo>();

        [NotMapped]
        public DateTime? CurrentDay 
        { 
            get => __currentDay; 
            set
            {
                __currentDay = value;
                DisplayCurrentDay();
            }
        }

        public DBContext DBContext { get; private set ; }

        private void DisplayCurrentDay()
        {
            if(CurrentDay != null)
            {

            }
        }

        private DateTime? __currentDay = null;
    }
}
