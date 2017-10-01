using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AustinHarris.JsonRpc;

namespace CFLServer
{
    public class CFLServerContext : JsonRpcService
    {
        [JsonRpcMethod]
        private bool Ping()
        {
            return true;
        }
        
    }
}
