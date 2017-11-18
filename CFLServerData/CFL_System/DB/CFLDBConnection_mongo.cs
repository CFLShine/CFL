using MSTD.Mongo;

namespace CFL_1.CFL_System.DB
{
    public class CFLDBConnection_mongo : ShDbConnection
    {

        #region instance

        public static CFLDBConnection_mongo Instance
        {
            get
            {
                if(__instance == null)
                {
                    __instance = new CFLDBConnection_mongo();
                }
                return __instance;
            }
        }

        private CFLDBConnection_mongo()
            :base("cfl_data")
        {
            Connect();
        }

        private static CFLDBConnection_mongo __instance = null;

        #endregion instance
    }
}
